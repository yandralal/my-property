import { Component, Output, EventEmitter, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PropertyService } from '../services/property.service';

@Component({
  selector: 'app-property-transaction-form',
  templateUrl: './property-transaction-form.component.html',
  styleUrls: ['./property-form.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule]
})
export class PropertyTransactionFormComponent implements OnInit {
  isSubmitting = false;
  @Input() propertyList: { id: number; title: string }[] = [];
  @Input() selectedPropertyId: number | null = null;
  @Input() transaction: any = null;
  @Output() closeModal = new EventEmitter<void>();
  @Output() success = new EventEmitter<string>();

  transactionForm: FormGroup;
  transactionTypes = ['Credit', 'Debit'];
  paymentModes = ['Cash', 'Cheque', 'Bank Transfer', 'Other'];

  constructor(private fb: FormBuilder, private propertyService: PropertyService) {
    const today = new Date().toISOString().slice(0, 10);
    this.transactionForm = this.fb.group({
      propertyId: [{ value: '', disabled: !!this.selectedPropertyId }, Validators.required],
      buyAmount: [{ value: '', disabled: true }],
      totalLoan: [{ value: '', disabled: true }],
      amountPaidTillDate: [{ value: '', disabled: true }],
      transactionType: ['Debit', Validators.required],
      amount: [null, [Validators.required, Validators.min(1)]],
      balanceAmount: [{ value: '', disabled: true }],
      paymentMethod: [this.paymentModes[0], Validators.required],
      referenceNumber: [''],
      notes: [''],
      transactionDate: [today, Validators.required]
    });
  }

  ngOnInit() {
    const formatINR = (value: any) => {
      if (value == null || value === '') return '';
      const num = typeof value === 'string' ? value.replace(/,/g, '') : value;
      if (isNaN(num) || num === '') return '';
      return Number(num).toLocaleString('en-IN');
    };

    const patchFormattedAmounts = () => {
      ['buyAmount', 'totalLoan', 'amountPaidTillDate', 'balanceAmount', 'amount'].forEach(field => {
        const ctrl = this.transactionForm.get(field);
        if (ctrl) {
          let raw = ctrl.value;
          // For balanceAmount, ensure 0 is shown as '0' and not formatted
          if (field === 'balanceAmount') {
            if (raw === 0 || raw === '0') {
              ctrl.setValue('0', { emitEvent: false });
              return;
            }
          }
          // Only format if not empty and not '0'
          if (raw !== '' && !(field === 'balanceAmount' && raw === '0')) {
            ctrl.setValue(formatINR(raw), { emitEvent: false });
          }
        }
      });
    };

    patchFormattedAmounts();
    ['buyAmount', 'totalLoan', 'amountPaidTillDate', 'balanceAmount', 'amount'].forEach(field => {
      this.transactionForm.get(field)?.valueChanges.subscribe(() => {
        patchFormattedAmounts();
      });
    });

    // Outstanding balance calculation
    this.transactionForm.get('amount')?.valueChanges.subscribe((txnAmount) => {
      const buyAmountRaw = parseFloat((this.transactionForm.get('buyAmount')?.value || '0').toString().replace(/,/g, ''));
      const totalLoanRaw = parseFloat((this.transactionForm.get('totalLoan')?.value || '0').toString().replace(/,/g, ''));
      const paidTillDateRaw = parseFloat((this.transactionForm.get('amountPaidTillDate')?.value || '0').toString().replace(/,/g, ''));
      const txnAmountRaw = parseFloat((txnAmount || '0').toString().replace(/,/g, ''));
      let remaining = buyAmountRaw - totalLoanRaw - (paidTillDateRaw + txnAmountRaw);
      // If outstanding balance is 0, show 0
      if (remaining === 0) {
        this.transactionForm.get('balanceAmount')?.setValue('0');
      } else {
        this.transactionForm.get('balanceAmount')?.setValue(remaining);
      }
    });

    if (!this.propertyList || this.propertyList.length === 0) {
      this.propertyService.getActiveProperties().subscribe({
        next: (props) => {
          this.propertyList = props.map((p: any) => ({ id: p.id, title: p.title }));
        },
        error: (err) => {
          console.error('Failed to fetch properties:', err);
        }
      });
    }
    // If selectedPropertyId is set, set and disable dropdown, and fetch info
    if (this.selectedPropertyId) {
      this.transactionForm.get('propertyId')?.setValue(this.selectedPropertyId);
      this.transactionForm.get('propertyId')?.disable();
      this.fetchTransactionInfo(this.selectedPropertyId);
    } else {
      // Enable dropdown if not selected
      this.transactionForm.get('propertyId')?.enable();
    }
    // On property change, fetch transaction info
    this.transactionForm.get('propertyId')?.valueChanges.subscribe((propertyId) => {
      if (propertyId) {
        this.fetchTransactionInfo(propertyId);
      }
    });
  }

  fetchTransactionInfo(propertyId: number) {
    if (!propertyId) return;
    this.propertyService.getPropertyTransactionInfo(propertyId).subscribe({
      next: (info: any) => {
        let balance = info.outstandingBalance;
        // Handle 0, '0', 0.00, '0.00' as zero
        if (balance === 0 || balance === '0' || balance === 0.00 || balance === '0.00') {
          balance = '0';
        }
        let paidTillDate = (info.paidTillDate === null || info.paidTillDate === undefined) ? 0 : info.paidTillDate;
        this.transactionForm.patchValue({
          buyAmount: info.purchaseAmount || '',
          totalLoan: info.totalLoan || '',
          amountPaidTillDate: paidTillDate,
          balanceAmount: balance || ''
        });
      },
      error: (err: any) => {
        console.error('Failed to fetch transaction info:', err);
      }
    });
  }

  onSubmit() {
    if (this.isSubmitting) return;
    if (this.transactionForm.valid) {
      this.isSubmitting = true;
      const formValue = this.transactionForm.getRawValue();
      let txnDate = formValue.transactionDate;
      // If only date is provided (YYYY-MM-DD), append current time
      if (/^\d{4}-\d{2}-\d{2}$/.test(txnDate)) {
        const now = new Date();
        const timeStr = now.toTimeString().slice(0,8); // HH:MM:SS
        txnDate = `${txnDate}T${timeStr}`;
      }
      const payload = {
        transactionId: 0,
        propertyId: formValue.propertyId || 0,
        transactionDate: new Date(txnDate).toISOString(),
        amount: parseFloat((formValue.amount || '0').toString().replace(/,/g, '')),
        paymentMethod: formValue.paymentMethod,
        referenceNumber: formValue.referenceNumber || '',
        transactionType: formValue.transactionType,
        notes: formValue.notes || ''
      };
      this.propertyService.createPropertyTransaction(payload).subscribe({
        next: () => {
          this.success.emit('Transaction saved successfully!');
          this.isSubmitting = false;
        },
        error: () => {
          this.success.emit('Failed to save transaction.');
          this.isSubmitting = false;
        }
      });
    }
  }

  closeModalClicked() {
    this.closeModal.emit();
  }
}

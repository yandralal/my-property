import { Component, Output, EventEmitter, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PropertyService } from '../services/property.service';
import { MessageBoxComponent } from '../shared/message-box.component';
import { PropertyTransactionsListComponent } from './property-transactions-list.component';

@Component({
  selector: 'app-property-transaction-form',
  templateUrl: './property-transaction-form.component.html',
  styleUrls: ['./property-form.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule]
})
export class PropertyTransactionFormComponent implements OnInit {
  // Helper to fetch transaction info and patch form fields
  fetchTransactionInfo(propertyId: number) {
    if (!propertyId) return;
    this.propertyService.getPropertyTransactionInfo(propertyId).subscribe({
      next: (info: any) => {
        this.transactionForm.patchValue({
          buyAmount: info.buyAmount || '',
          totalLoan: info.totalLoan || '',
          amountPaidTillDate: info.amountPaidTillDate || '',
          balanceAmount: info.balanceAmount || ''
        });
      },
      error: (err: any) => {
        console.error('Failed to fetch transaction info:', err);
      }
    });
  }
  // Inputs
  @Input() propertyList: { id: number; title: string; selected?: boolean; buyAmount?: number; totalLoan?: number; amountPaidTillDate?: number; balanceAmount?: number }[] = [];
  @Input() selectedPropertyId: number | null = null;
  @Input() viewMode: 'view' | 'edit' = 'view';
  @Input() transaction: any = null;
  // Outputs
  @Output() closeModal = new EventEmitter<void>();
  @Output() success = new EventEmitter<string>();

  // Form and UI State
  transactionForm: FormGroup;
  messageBoxVisible = false;
  messageText = '';
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
      console.log('PropertyTransactionFormComponent ngOnInit', {
        transaction: this.transaction,
        propertyList: this.propertyList,
        selectedPropertyId: this.selectedPropertyId,
        viewMode: this.viewMode
      });
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
          const raw = ctrl.value;
          ctrl.setValue(formatINR(raw), { emitEvent: false });
        }
      });
    };

    patchFormattedAmounts();
    ['buyAmount', 'totalLoan', 'amountPaidTillDate', 'balanceAmount', 'amount'].forEach(field => {
      this.transactionForm.get(field)?.valueChanges.subscribe(() => {
        patchFormattedAmounts();
      });
    });

    // --- Outstanding balance calculation ---
    this.transactionForm.get('amount')?.valueChanges.subscribe((txnAmount) => {
      const buyAmountRaw = parseFloat((this.transactionForm.get('buyAmount')?.value || '0').toString().replace(/,/g, ''));
      const totalLoanRaw = parseFloat((this.transactionForm.get('totalLoan')?.value || '0').toString().replace(/,/g, ''));
      const paidTillDateRaw = parseFloat((this.transactionForm.get('amountPaidTillDate')?.value || '0').toString().replace(/,/g, ''));
      const txnAmountRaw = parseFloat((txnAmount || '0').toString().replace(/,/g, ''));
      const remaining = buyAmountRaw - totalLoanRaw - (paidTillDateRaw + txnAmountRaw);
      this.transactionForm.get('balanceAmount')?.setValue(remaining);
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

  onSubmit() {
    if (this.transactionForm.valid) {
      const formValue = this.transactionForm.getRawValue();
      const payload = {
        transactionId: 0,
        propertyId: formValue.propertyId || 0,
        transactionDate: new Date(formValue.transactionDate).toISOString(),
        amount: parseFloat((formValue.amount || '0').toString().replace(/,/g, '')),
        paymentMethod: formValue.paymentMethod,
        referenceNumber: formValue.referenceNumber || '',
        transactionType: formValue.transactionType,
        notes: formValue.notes || ''
      };
      this.propertyService.createPropertyTransaction(payload).subscribe({
        next: () => {
          this.showMessage('Transaction saved successfully!');
          this.success.emit();
        },
        error: () => {
          this.showMessage('Failed to save transaction.');
        }
      });
    }
}

closeModalClicked() {
    this.closeModal.emit();
  }

  showMessage(msg: string) {
    this.messageText = msg;
    this.messageBoxVisible = true;
  }

  closeMessageBox() {
    this.messageBoxVisible = false;
  }

  populatePropertyFields(prop: any) {
    this.transactionForm.patchValue({
      buyAmount: prop.buyAmount || '',
      totalLoan: prop.totalLoan || '',
      amountPaidTillDate: prop.amountPaidTillDate || '',
      balanceAmount: prop.balanceAmount || ''
    });
  }
}

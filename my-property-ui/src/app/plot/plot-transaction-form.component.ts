import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, Validators, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { PlotService } from '../services/plot.service';

@Component({
  selector: 'app-plot-transaction-form',
  templateUrl: './plot-transaction-form.component.html',
  styleUrls: ['./plot-form.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class PlotTransactionFormComponent implements OnInit {
  isSubmitting = false;
  // Inputs & Outputs
  @Input() plotList: {
    id: number;
    title?: string;
    plotNumber?: string;
    buyAmount?: number;
    amountPaidTillDate?: number;
    balanceAmount?: number;
  }[] = [];
  @Input() selectedPlotId: number | null = null;
  @Input() transaction: any = null;
  @Output() closeModal = new EventEmitter<void>();
  @Output() success = new EventEmitter<string>();

  // Form & Constants
  transactionForm: FormGroup;

  onlyNumbers(event: any): void {
    const input = event.target;
    let value = input.value.replace(/[^0-9]/g, '');
    if (value) {
      value = this.formatIndianNumber(value);
    }
    input.value = value;
  }

  formatIndianNumber(num: string): string {
    if (!num) return '';
    const numStr = num.toString();
    let lastThree = numStr.substring(numStr.length - 3);
    const otherNumbers = numStr.substring(0, numStr.length - 3);
    if (otherNumbers !== '') {
      lastThree = ',' + lastThree;
    }
    return otherNumbers.replace(/\B(?=(\d{2})+(?!\d))/g, ',') + lastThree;
  }
  transactionTypes = ['Credit', 'Debit'];
  paymentModes = ['Cash', 'Cheque', 'Bank Transfer', 'Other'];

  constructor(private fb: FormBuilder, private plotService: PlotService) {
    const today = new Date().toISOString().slice(0, 10);
    this.transactionForm = this.fb.group({
      plot: [{ value: '', disabled: true }, Validators.required],
      buyAmount: [{ value: '', disabled: true }],
      amountPaidTillDate: [{ value: '', disabled: true }],
      transactionType: ['Credit', Validators.required],
      amount: [null, [Validators.required, Validators.min(1)]],
      balanceAmount: [{ value: '', disabled: true }],
      paymentMethod: [this.paymentModes[0], Validators.required],
      referenceNumber: [''],
      notes: [''],
      transactionDate: [today, Validators.required]
    });
  }

  ngOnInit() {
    this.patchFormattedAmounts();
    this.setupAmountFormatting();
    this.setupBalanceCalculation();

    if (this.selectedPlotId) {
      this.fetchTransactionInfo(this.selectedPlotId);
    }

    if (this.transaction) {
      this.patchTransactionValues();
      this.patchFormattedAmounts();
    }
    this.transactionForm.updateValueAndValidity();
  }

  getPlotDisplayValue(): string {
    return this.transactionForm?.get('plot')?.value || '';
  }

  onSubmit() {
    if (this.isSubmitting || !this.transactionForm.valid) return;
    this.isSubmitting = true;
    const formValue = this.transactionForm.getRawValue();
    let txnDate = formValue.transactionDate;
    if (/^\d{4}-\d{2}-\d{2}$/.test(txnDate)) {
      const now = new Date();
      txnDate = `${txnDate}T${now.toTimeString().slice(0,8)}`;
    }
    const payload = {
      transactionId: this.transaction?.transactionId || 0,
      plotId: this.selectedPlotId || 0,
      transactionDate: new Date(txnDate).toISOString(),
      amount: parseFloat((formValue.amount || '0').toString().replace(/,/g, '')),
      paymentMethod: formValue.paymentMethod,
      referenceNumber: formValue.referenceNumber || '',
      transactionType: formValue.transactionType,
      notes: formValue.notes || ''
    };
    this.plotService.createPlotTransaction(payload).subscribe({
      next: () => {
        this.success.emit('Transaction saved successfully!');
        this.closeModal.emit();
        this.isSubmitting = false;
      },
      error: () => {
        this.success.emit('Failed to save transaction.');
        this.isSubmitting = false;
      }
    });
  }

  closeModalClicked() {
    this.closeModal.emit();
  }

  // --- Private/Utility Methods ---

  public formatINR(value: any): string {
    if (value == null || value === '') return '';
    const num = typeof value === 'string' ? parseFloat(value.replace(/,/g, '')) : value;
    if (isNaN(num)) return '';
    return num.toLocaleString('en-IN', { maximumFractionDigits: 0 });
  }

  private patchFormattedAmounts() {
    ['buyAmount', 'totalLoan', 'amountPaidTillDate', 'balanceAmount', 'amount'].forEach(field => {
      const ctrl = this.transactionForm.get(field);
      if (ctrl) {
        let raw = ctrl.value;
        if (field === 'balanceAmount' && (raw === 0 || raw === '0')) {
          ctrl.setValue('0', { emitEvent: false });
        } else if (raw !== '' && !(field === 'balanceAmount' && raw === '0')) {
          ctrl.setValue(this.formatINR(raw), { emitEvent: false });
        }
      }
    });
  }

  private setupAmountFormatting() {
    ['buyAmount', 'totalLoan', 'amountPaidTillDate', 'balanceAmount', 'amount'].forEach(field => {
      this.transactionForm.get(field)?.valueChanges.subscribe(() => {
        this.patchFormattedAmounts();
      });
    });
  }

  private setupBalanceCalculation() {
    this.transactionForm.get('amount')?.valueChanges.subscribe((txnAmount) => {
      const buyAmountRaw = parseFloat((this.transactionForm.get('buyAmount')?.value || '0').toString().replace(/,/g, ''));
      const totalLoanRaw = parseFloat((this.transactionForm.get('totalLoan')?.value || '0').toString().replace(/,/g, ''));
      const paidTillDateRaw = parseFloat((this.transactionForm.get('amountPaidTillDate')?.value || '0').toString().replace(/,/g, ''));
      const txnAmountRaw = parseFloat((txnAmount || '0').toString().replace(/,/g, ''));
      const remaining = buyAmountRaw - totalLoanRaw - (paidTillDateRaw + txnAmountRaw);
      this.transactionForm.get('balanceAmount')?.setValue(remaining === 0 ? '0' : remaining);
    });
  }

  private fetchTransactionInfo(plotId: number) {
    if (!plotId) return;
    this.plotService.getPlotTransactionInfo(plotId).subscribe({
      next: (info: any) => {
        let balance = (info.outstandingBalance === 0 || info.outstandingBalance === '0' || info.outstandingBalance === 0.00 || info.outstandingBalance === '0.00')
          ? '0' : info.outstandingBalance;
        let paidTillDate = (info.paidTillDate == null) ? 0 : info.paidTillDate;
        this.transactionForm.patchValue({
          plot: info.plot,
          buyAmount: info.saleAmount || '',
          amountPaidTillDate: paidTillDate,
          balanceAmount: balance || ''
        });
        this.transactionForm.get('plot')?.disable();
      },
      error: (err: any) => {
        console.error('Failed to fetch transaction info:', err);
      }
    });
  }

  private patchTransactionValues() {
    this.transactionForm.patchValue({
      plot: this.transaction.plot,
      buyAmount: this.transaction.buyAmount,
      amountPaidTillDate: this.transaction.amountPaidTillDate,
      transactionType: this.transaction.transactionType,
      amount: this.transaction.amount,
      balanceAmount: this.transaction.balanceAmount,
      paymentMethod: this.transaction.paymentMethod,
      referenceNumber: this.transaction.referenceNumber,
      notes: this.transaction.notes,
      transactionDate: this.transaction.transactionDate?.slice(0, 10)
    });
  }
}

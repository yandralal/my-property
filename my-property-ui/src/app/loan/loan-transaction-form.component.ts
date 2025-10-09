import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PropertyService } from '../services/property.service';
import { AgentService } from '../services/agent.service';

@Component({
  selector: 'app-loan-transaction-form',
  templateUrl: './loan-transaction-form.component.html',
  styleUrls: ['./loan-transaction-form.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class LoanTransactionFormComponent implements OnInit {
  @Input() transaction: any;
  @Input() loanDetails: any;
  @Output() closeModal = new EventEmitter<void>();
  @Output() success = new EventEmitter<any>();

  loanTransactionForm!: FormGroup;
  lenders: string[] = [];
  transactionTypes: string[] = ['Credit', 'Debit'];
  isSubmitting = false;

  constructor(
    private fb: FormBuilder,
    private propertyService: PropertyService,
    private agentService: AgentService
  ) {}

    // Returns yyyy-MM-dd format for input type="date"
    getTodayDateString(): string {
      const today = new Date();
      const yyyy = today.getFullYear();
      const mm = String(today.getMonth() + 1).padStart(2, '0');
      const dd = String(today.getDate()).padStart(2, '0');
      return `${yyyy}-${mm}-${dd}`;
    }

    formatINR(value: any): string {
      if (value === null || value === undefined || value === '') return '';
      let num = Number(value.toString().replace(/,/g, ''));
      if (isNaN(num)) return value;
      return num.toLocaleString('en-IN', { maximumFractionDigits: 2 });
    }

    patchFormattedAmounts() {
      ['totalInterest', 'totalInterestPaid', 'totalPrincipalPaid', 'totalPrincipal', 'amount', 'balance', 'loanAmount'].forEach(field => {
        const ctrl = this.loanTransactionForm.get(field);
        if (ctrl) {
          let raw = ctrl.value;
          if (raw === null || raw === undefined || raw === '' || raw === 0 || raw === '0' || raw === '0.00') {
            ctrl.setValue('0', { emitEvent: false });
          } else {
            ctrl.setValue(this.formatINR(raw), { emitEvent: false });
          }
        }
      });
    }


  ngOnInit() {
    this.loanTransactionForm = this.fb.group({
      property: [{ value: '', disabled: true }],
      lenderName: [{ value: '', disabled: true }],
      loanAmount: [{ value: '', disabled: true }],
      payingFor: ['', Validators.required],
      interest: [''],
      totalInterest: [{ value: '', disabled: true }],
      totalInterestPaid: [{ value: '', disabled: true }],
      totalPrincipalPaid: [{ value: '', disabled: true }],
      totalPrincipal: [{ value: '', disabled: true }],
      amount: ['', [Validators.required, Validators.min(1)]],
      balance: [{ value: '', disabled: true }],
      paymentMethod: ['Cash', Validators.required],
      referenceNumber: [''],
      notes: [''],
      transactionDate: [this.getTodayDateString(), Validators.required],
      transactionType: ['Debit', Validators.required]
    });
    // Patch loan details if provided
    if (this.loanDetails) {
      this.loanTransactionForm.patchValue({
        property: this.loanDetails.propertyName || '',
        lenderName: this.loanDetails.lenderName || '',
        loanAmount: this.loanDetails.loanAmount || '',
        totalInterest: this.loanDetails.totalInterest || '',
        totalPrincipal: this.loanDetails.totalRepayment || '',
        balance: this.calculateOutstandingBalance(this.loanDetails, 0)
      });
    }
    // Optionally patch values if editing
    if (this.transaction) {
      this.loanTransactionForm.patchValue(this.transaction);
    }

    // Update balance when amount changes
    this.loanTransactionForm.get('amount')?.valueChanges.subscribe((amount: any) => {
      let validAmount = Number(amount);
      if (isNaN(validAmount) || amount === '' || amount === null || amount === undefined) {
        validAmount = 0;
      }
      if (this.loanDetails) {
        const newBalance = this.calculateOutstandingBalance(this.loanDetails, validAmount);
        this.loanTransactionForm.patchValue({ balance: newBalance });
      }
      this.patchFormattedAmounts();
    });
    // Format amounts on init
    this.patchFormattedAmounts();
  }

  calculateOutstandingBalance(loanDetails: any, transactionAmount: number): number {
    // Outstanding = (loanAmount + totalInterest) - (totalPaid + transactionAmount)
    const loanAmount = Number(loanDetails?.loanAmount ?? 0);
    const totalInterest = Number(loanDetails?.totalInterest ?? 0);
    const totalPaid = Number(loanDetails?.totalPaid ?? 0);
    return (loanAmount + totalInterest) - (totalPaid + Number(transactionAmount ?? 0));
  }

  onSubmit() {
    if (this.loanTransactionForm.invalid) {
      this.loanTransactionForm.markAllAsTouched();
      return;
    }
    this.isSubmitting = true;
    this.success.emit(this.loanTransactionForm.getRawValue());
    this.closeModal.emit();
    this.isSubmitting = false;
  }

  onCancel() {
    this.closeModal.emit();
  }
}

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
      ['totalInterest', 'totalInterestPaid', 'totalPrinciplePaid', 'totalPrinciple', 'amount', 'balance', 'loanAmount'].forEach(field => {
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
      totalPrinciplePaid: [{ value: '', disabled: true }],
      totalPrinciple: [{ value: '', disabled: true }],
      amount: ['', [Validators.required, Validators.min(1), this.validateTransactionAmount.bind(this)]],
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
        totalInterestPaid: this.loanDetails.totalInterestPaid || 0,
        totalPrinciplePaid: this.loanDetails.totalPrincipalPaid || 0,
        totalPrinciple: this.loanDetails.loanAmount || '',
        balance: this.calculateOutstandingBalance(this.loanDetails, 0)
      });
    }
    // Optionally patch values if editing
    if (this.transaction) {
      this.loanTransactionForm.patchValue(this.transaction);
    }

    // Update balance when amount changes
    this.loanTransactionForm.get('amount')?.valueChanges.subscribe((amount: any) => {
      // Remove formatting (commas) before converting to number
      let amountStr = String(amount ?? '').replace(/,/g, '');
      let validAmount = Number(amountStr);
      if (isNaN(validAmount) || amount === '' || amount === null || amount === undefined) {
        validAmount = 0;
      }
      if (this.loanDetails) {
        const newBalance = this.calculateOutstandingBalance(this.loanDetails, validAmount);
        this.loanTransactionForm.patchValue({ balance: this.formatINR(newBalance) }, { emitEvent: false });
      }
      
      // Format the amount field itself
      if (validAmount > 0) {
        const formattedAmount = this.formatINR(validAmount);
        if (formattedAmount !== amount) {
          this.loanTransactionForm.patchValue({ amount: formattedAmount }, { emitEvent: false });
        }
      }
    });
    
    // Revalidate amount when payingFor changes
    this.loanTransactionForm.get('payingFor')?.valueChanges.subscribe(() => {
      this.loanTransactionForm.get('amount')?.updateValueAndValidity();
      
      // Recalculate balance when payingFor changes
      const currentAmount = this.loanTransactionForm.get('amount')?.value;
      if (currentAmount && this.loanDetails) {
        const amountStr = String(currentAmount ?? '').replace(/,/g, '');
        const validAmount = Number(amountStr);
        if (!isNaN(validAmount) && validAmount > 0) {
          const newBalance = this.calculateOutstandingBalance(this.loanDetails, validAmount);
          this.loanTransactionForm.patchValue({ balance: this.formatINR(newBalance) }, { emitEvent: false });
        }
      }
    });
    
    // Format amounts on init
    this.patchFormattedAmounts();
  }

  validateTransactionAmount(control: any) {
    if (!this.loanDetails || !this.loanTransactionForm) return null;
    
    const amountStr = String(control.value ?? '').replace(/,/g, '');
    const txnAmount = Number(amountStr);
    
    if (isNaN(txnAmount) || txnAmount <= 0) return null;
    
    const loanAmount = Number(String(this.loanDetails?.loanAmount ?? 0).replace(/,/g, ''));
    const totalInterest = Number(String(this.loanDetails?.totalInterest ?? 0).replace(/,/g, ''));
    const totalPaid = Number(String(this.loanDetails?.totalPaid ?? 0).replace(/,/g, ''));
    const totalInterestPaid = Number(String(this.loanDetails?.totalInterestPaid ?? 0).replace(/,/g, ''));
    const totalPrincipalPaid = Number(String(this.loanDetails?.totalPrincipalPaid ?? 0).replace(/,/g, ''));
    
    // Check if transaction exceeds overall outstanding
    const totalRepayable = loanAmount + totalInterest;
    const outstanding = totalRepayable - totalPaid;
    
    if (txnAmount > outstanding) {
      return { exceedsOutstanding: { outstanding, txnAmount } };
    }
    
    // Check based on what user is paying for (Interest or Principal)
    const payingFor = this.loanTransactionForm.get('payingFor')?.value;
    
    if (payingFor === 'Interest') {
      const remainingInterest = totalInterest - totalInterestPaid;
      if (txnAmount > remainingInterest) {
        return { exceedsInterest: { total: totalInterest, paid: totalInterestPaid, remaining: remainingInterest } };
      }
    } else if (payingFor === 'Principle') {
      const remainingPrincipal = loanAmount - totalPrincipalPaid;
      if (txnAmount > remainingPrincipal) {
        return { exceedsPrincipal: { total: loanAmount, paid: totalPrincipalPaid, remaining: remainingPrincipal } };
      }
    }
    
    return null;
  }

  calculateOutstandingBalance(loanDetails: any, transactionAmount: number): number {
    // Remove formatting and convert to numbers
    const loanAmount = Number(String(loanDetails?.loanAmount ?? 0).replace(/,/g, ''));
    const totalInterest = Number(String(loanDetails?.totalInterest ?? 0).replace(/,/g, ''));
    const totalInterestPaid = Number(String(loanDetails?.totalInterestPaid ?? 0).replace(/,/g, ''));
    const totalPrincipalPaid = Number(String(loanDetails?.totalPrincipalPaid ?? 0).replace(/,/g, ''));
    const txnAmount = Number(transactionAmount ?? 0);
    
    // Get what user is paying for
    const payingFor = this.loanTransactionForm?.get('payingFor')?.value;
    
    if (payingFor === 'Interest') {
      // Outstanding Interest = Total Interest - Total Interest Paid - Current Transaction Amount
      return totalInterest - totalInterestPaid - txnAmount;
    } else if (payingFor === 'Principle') {
      // Outstanding Principal = Loan Amount - Total Principal Paid - Current Transaction Amount
      return loanAmount - totalPrincipalPaid - txnAmount;
    }
    
    // Default: calculate total outstanding (Interest + Principal)
    const totalPaid = Number(String(loanDetails?.totalPaid ?? 0).replace(/,/g, ''));
    const totalRepayable = loanAmount + totalInterest;
    const totalPaidAfterTransaction = totalPaid + txnAmount;
    
    return totalRepayable - totalPaidAfterTransaction;
  }

  onSubmit() {
    if (this.loanTransactionForm.invalid) {
      this.loanTransactionForm.markAllAsTouched();
      return;
    }
    this.isSubmitting = true;
    
    const formData = this.loanTransactionForm.getRawValue();
    
    // If only date is provided (YYYY-MM-DD), append current time
    let txnDate = formData.transactionDate;
    if (/^\d{4}-\d{2}-\d{2}$/.test(txnDate)) {
      const now = new Date();
      const timeStr = now.toTimeString().slice(0, 8); // HH:MM:SS
      txnDate = `${txnDate}T${timeStr}`;
    }
    
    // Add propertyLoanId and propertyId to the form data
    const payload = {
      ...formData,
      transactionDate: new Date(txnDate).toISOString(),
      propertyLoanId: this.loanDetails?.id,
      propertyId: this.loanDetails?.propertyId
    };
    
    this.success.emit(payload);
    // Don't close immediately - let parent handle closing after successful API call
  }

  onCancel() {
    this.closeModal.emit();
  }

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
}

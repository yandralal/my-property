import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-loan-transaction-form',
  template: `<div>Loan Transaction Form (Stub)</div>
    <button (click)="closeModal.emit()">Close</button>`,
  standalone: true,
  imports: [CommonModule]
})
export class LoanTransactionFormComponent {
  @Input() transaction: any;
  @Input() viewMode: boolean = false;
  @Input() loanList: any[] = [];
  @Output() closeModal = new EventEmitter<void>();
  @Output() success = new EventEmitter<string>();
}

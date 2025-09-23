import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-loan-transactions-list',
    templateUrl: './loan-transactions-list.component.html',
    styleUrls: ['./loan-transactions-list.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class LoanTransactionsListComponent {
  loading = false;
  error: string = '';
  transactions: any[] = [];
  closeModalClicked() {
    this.closeModal.emit();
  }
  @Input() loanId: number | null = null;
  @Output() closeModal = new EventEmitter<void>();
}

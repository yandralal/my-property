import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InrFormatPipe } from '../shared/inr-format.pipe';

@Component({
  selector: 'app-loan-transactions-list',
  templateUrl: './loan-transactions-list.component.html',
  styleUrls: ['./loan-transactions-list.component.css'],
  standalone: true,
  imports: [CommonModule, InrFormatPipe]
})
export class LoanTransactionsListComponent {
  @Input() transactions: any[] = [];
  @Input() propertyLoanId: number | null = null;
  @Output() requestDeleteTransaction = new EventEmitter<any>();

  onDeleteTransaction(txn: any) {
    this.requestDeleteTransaction.emit(txn);
  }
}

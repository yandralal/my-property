import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InrFormatPipe } from '../shared/inr-format.pipe';

@Component({
  selector: 'app-plot-transactions-list',
  templateUrl: './plot-transactions-list.component.html',
  standalone: true,
  imports: [CommonModule, InrFormatPipe]
})
export class PlotTransactionsListComponent {
  @Input() transactions: any[] = [];
  @Output() requestDeleteTransaction = new EventEmitter<any>();
  
  // Pagination
  currentPage = 1;
  pageSize = 10;
  
  get paginatedTransactions(): any[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    return this.transactions.slice(startIndex, endIndex);
  }
  
  get totalPages(): number {
    return Math.ceil(this.transactions.length / this.pageSize);
  }
  
  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
    }
  }

  onDeleteTransaction(txn: any) {
    this.requestDeleteTransaction.emit(txn);
  }
}

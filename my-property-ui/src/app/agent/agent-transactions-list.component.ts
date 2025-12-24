import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InrFormatPipe } from "../shared/inr-format.pipe";

@Component({
  selector: 'app-agent-transactions-list',
  templateUrl: './agent-transactions-list.component.html',
  styleUrls: ['./agent-transactions-list.component.css'],
  standalone: true,
  imports: [CommonModule, InrFormatPipe]
})
export class AgentTransactionsListComponent {
  @Input() transactions: any[] = [];
  @Input() agentId: number | null = null;
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

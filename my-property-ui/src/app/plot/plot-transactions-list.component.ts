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

  onDeleteTransaction(txn: any) {
    this.requestDeleteTransaction.emit(txn);
  }
}

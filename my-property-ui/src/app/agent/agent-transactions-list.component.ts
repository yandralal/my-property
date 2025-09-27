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

  onDeleteTransaction(txn: any) {
    this.requestDeleteTransaction.emit(txn);
  }
}

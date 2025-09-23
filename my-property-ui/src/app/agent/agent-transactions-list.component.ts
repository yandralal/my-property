import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-agent-transactions-list',
    templateUrl: './agent-transactions-list.component.html',
    styleUrls: ['./agent-transactions-list.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class AgentTransactionsListComponent {
  loading = false;
  error: string = '';
  @Input() transactions: any[] = [];
  closeModalClicked() {
    this.closeModal.emit();
  }
  @Input() agentId: number | null = null;
  @Output() closeModal = new EventEmitter<void>();
}

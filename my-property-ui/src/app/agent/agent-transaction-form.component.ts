import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-agent-transaction-form',
  template: `<div>Agent Transaction Form (Stub)</div>
    <button (click)="closeModal.emit()">Close</button>`,
  standalone: true,
  imports: [CommonModule]
})
export class AgentTransactionFormComponent {
  @Input() transaction: any;
  @Input() viewMode: boolean = false;
  @Input() agentList: any[] = [];
  @Output() closeModal = new EventEmitter<void>();
  @Output() success = new EventEmitter<string>();
}

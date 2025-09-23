import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-misc-transaction-form',
  template: `<div>Misc Transaction Form (Stub)</div>
    <button (click)="closeModal.emit()">Close</button>`,
  standalone: true,
  imports: [CommonModule]
})
export class MiscTransactionFormComponent {
  @Input() transaction: any;
  @Input() viewMode: boolean = false;
  @Input() miscList: any[] = [];
  @Output() closeModal = new EventEmitter<void>();
  @Output() success = new EventEmitter<string>();
}

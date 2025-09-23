import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-plot-transaction-form',
  template: `<div>Plot Transaction Form (Stub)</div>
    <button (click)="closeModal.emit()">Close</button>`,
  standalone: true,
  imports: [CommonModule]
})
export class PlotTransactionFormComponent {
  @Input() transaction: any;
  @Input() viewMode: boolean = false;
  @Input() plotList: any[] = [];
  @Output() closeModal = new EventEmitter<void>();
  @Output() success = new EventEmitter<string>();
}

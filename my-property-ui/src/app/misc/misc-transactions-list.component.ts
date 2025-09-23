import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-misc-transactions-list',
    templateUrl: './misc-transactions-list.component.html',
    styleUrls: ['./misc-transactions-list.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class MiscTransactionsListComponent {
  loading = false;
  error: string = '';
  transactions: any[] = [];
  closeModalClicked() {
    this.closeModal.emit();
  }
  @Input() miscId: number | null = null;
  @Output() closeModal = new EventEmitter<void>();
}

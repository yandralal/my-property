import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { InrFormatPipe } from '../shared/inr-format.pipe';
import { PropertyService } from '../services/property.service';
import { ConfirmDialogComponent } from '../shared/confirm-dialog.component';
import { MessageBoxComponent } from '../shared/message-box.component';
@Component({
  selector: 'app-misc-list',
  templateUrl: './misc-list.component.html',
  styleUrls: ['./misc-list.component.css'],
  standalone: true,
  imports: [CommonModule, DatePipe, InrFormatPipe, ConfirmDialogComponent, MessageBoxComponent]
})
export class MiscListComponent {
  @Input() miscList: any[] = [];
  @Output() miscEdited = new EventEmitter<any>();
  @Output() addMisc = new EventEmitter<void>();
  @Output() selectMisc = new EventEmitter<any>();
  @Output() miscDeleted = new EventEmitter<number>();

  selectedMiscId: number | null = null;
  confirmDeleteMiscVisible = false;
  miscToDelete: any = null;
  messageBoxVisible = false;
  messageText = '';
  viewMiscModalVisible = false;
  selectedMiscForView: any = null;
  @Output() viewMisc = new EventEmitter<any>();

  constructor(private propertyService: PropertyService) {}

  ngOnInit() {
    this.loadMisc();
  }

  private loadMisc() {
    // Placeholder: If you have an API for misc items, call it here. For now assume miscList is input.
  }

  onSelectMisc(item: any) {
    this.selectedMiscId = item.transactionId ?? item.id;
    this.selectMisc.emit(item);
  }

  onAddMiscTransaction() {
    this.addMisc.emit();
  }

  onViewMisc(item: any) {
    this.viewMisc.emit(item);
  }

  closeViewMiscModal() {
    this.viewMiscModalVisible = false;
    this.selectedMiscForView = null;
  }

  onDeleteMisc(item: any) {
    this.miscToDelete = item;
    this.confirmDeleteMiscVisible = true;
  }

  onConfirmDeleteMisc() {
    if (this.miscToDelete) {
      const id = this.miscToDelete.transactionId ?? this.miscToDelete.id;
      // Replace with API call if needed. For now emit event and show message
      this.miscDeleted.emit(id);
      this.showMessage('Record deleted successfully.');
      this.confirmDeleteMiscVisible = false;
      this.miscToDelete = null;
    }
  }

  onCancelDeleteMisc() {
    this.confirmDeleteMiscVisible = false;
    this.miscToDelete = null;
  }

  showMessage(msg: string) {
    this.messageText = msg;
    this.messageBoxVisible = true;
    setTimeout(() => {
      this.messageBoxVisible = false;
    }, 1500);
  }
}
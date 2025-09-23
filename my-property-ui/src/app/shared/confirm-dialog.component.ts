import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-confirm-dialog',
  template: `
    <div class="message-box-overlay confirm-dialog-overlay">
      <div class="message-box confirm-dialog-modal">
        <span class="message-text">{{ message }}</span>
        <div style="display:flex; gap:1em; margin-top:1.5rem;">
          <button class="message-close" (click)="onConfirm()">Yes</button>
          <button class="message-close" style="background:#64748b;" (click)="onCancel()">No</button>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .message-box-overlay.confirm-dialog-overlay {
      position: fixed;
      top: 0;
      left: 0;
      width: 100vw;
      height: 100vh;
      background: rgba(44,62,80,0.18);
      display: flex;
      align-items: center;
      justify-content: center;
      z-index: 999999 !important;
    }
    .message-box.confirm-dialog-modal {
      background: #fff;
      border-radius: 8px;
      box-shadow: 0 2px 12px rgba(44,62,80,0.18);
      padding: 2rem 2.5rem;
      display: flex;
      flex-direction: column;
      align-items: center;
      max-width: 600px;
      min-width: 400px;
      z-index: 1000000 !important;
      position: relative;
    }
    .message-text {
      font-size: 1.15rem;
      color: #222;
      margin-bottom: 1.5rem;
      text-align: center;
    }
    .message-close {
      background: #1e90ff;
      color: #fff;
      border: none;
      border-radius: 4px;
      padding: 0.5rem 1.5rem;
      font-size: 1rem;
      cursor: pointer;
      font-weight: 500;
      min-width: 80px;
    }
    .message-close:hover {
      background: #1565c0;
    }
  `],
  standalone: true
})
export class ConfirmDialogComponent {
  @Input() title: string = 'Confirm';
  @Input() message: string = 'Are you sure?';
  @Output() confirm = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  onConfirm() { this.confirm.emit(); }
  onCancel() { this.cancel.emit(); }
}

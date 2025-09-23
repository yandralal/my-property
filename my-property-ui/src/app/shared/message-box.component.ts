import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-message-box',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="message-box-overlay" *ngIf="show">
      <div class="message-box">
        <span class="message-text">{{ message }}</span>
        <button class="message-close" (click)="close.emit()">OK</button>
      </div>
    </div>
  `,
  styleUrls: ['./message-box.component.css']
})
export class MessageBoxComponent {
  @Input() show: boolean = false;
  @Input() message: string = '';
  @Output() close = new EventEmitter<void>();
}

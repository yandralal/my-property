import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-misc-form',
  templateUrl: './misc-form.component.html',
  styleUrls: ['./misc-form.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class MiscFormComponent implements OnInit {
  @Input() miscItem: any;
  @Output() closeModal = new EventEmitter<void>();
  @Output() success = new EventEmitter<any>();

  miscForm!: FormGroup;
  transactionTypes: string[] = ['Expense', 'Income', 'Other'];
  isSubmitting = false;
  isViewMode = false;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.miscForm = this.fb.group({
      recipient: ['', Validators.required],
      amount: ['', [Validators.required, Validators.min(1)]],
      transactionType: ['', Validators.required],
      paymentMethod: ['', Validators.required],
      referenceNumber: [''],
      notes: [''],
      transactionDate: [this.getTodayDateString(), Validators.required]
    });

    // Check if in view mode
    if (this.miscItem && this.miscItem.isViewMode) {
      this.isViewMode = true;
      this.miscForm.disable();
    }

    // If editing/viewing, patch the form with existing data
    if (this.miscItem) {
      this.miscForm.patchValue(this.miscItem);
    }

    // Update formatted amounts when amount changes
    this.miscForm.get('amount')?.valueChanges.subscribe(() => {
      this.patchFormattedAmounts();
    });

    // Format amounts on init
    this.patchFormattedAmounts();
  }

  formatINR(value: any): string {
    if (value === null || value === undefined || value === '') return '';
    let num = Number(value.toString().replace(/,/g, ''));
    if (isNaN(num)) return value;
    return num.toLocaleString('en-IN', { maximumFractionDigits: 2 });
  }

  patchFormattedAmounts() {
    ['amount'].forEach(field => {
      const ctrl = this.miscForm.get(field);
      if (ctrl) {
        let raw = ctrl.value;
        if (raw === null || raw === undefined || raw === '' || raw === 0 || raw === '0' || raw === '0.00') {
          ctrl.setValue('0', { emitEvent: false });
        } else {
          ctrl.setValue(this.formatINR(raw), { emitEvent: false });
        }
      }
    });
  }

  getTodayDateString(): string {
    const today = new Date();
    const yyyy = today.getFullYear();
    const mm = String(today.getMonth() + 1).padStart(2, '0');
    const dd = String(today.getDate()).padStart(2, '0');
    return `${yyyy}-${mm}-${dd}`;
  }

  onSubmit() {
    // Don't submit if in view mode
    if (this.isViewMode) {
      this.closeModal.emit();
      return;
    }

    if (this.miscForm.invalid) {
      this.miscForm.markAllAsTouched();
      return;
    }
    this.isSubmitting = true;

    const formValue = this.miscForm.getRawValue();
    const payload = {
      recipient: formValue.recipient,
      amount: parseFloat((formValue.amount || '').toString().replace(/,/g, '')),
      transactionType: formValue.transactionType,
      paymentMethod: formValue.paymentMethod,
      referenceNumber: formValue.referenceNumber || '',
      notes: formValue.notes || '',
      transactionDate: formValue.transactionDate ? new Date(formValue.transactionDate).toISOString() : new Date().toISOString()
    };

    this.success.emit(payload);
    this.closeModal.emit();
    this.isSubmitting = false;
  }

  onCancel() {
    this.closeModal.emit();
  }
}
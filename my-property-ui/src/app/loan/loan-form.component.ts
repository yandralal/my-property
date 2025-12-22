import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PropertyService } from '../services/property.service';
import { Property } from '../models/property.model';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-loan-form',
  templateUrl: './loan-form.component.html',
  styleUrls: ['./loan-form.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class LoanFormComponent {
  ngOnChanges() {
    if (this.loan && this.loanForm) {
      // Check if in view mode
      if (this.loan && this.loan.isViewMode) {
        this.isViewMode = true;
        this.loanForm.disable();
      }

      this.loanForm.patchValue({
        property: this.loan.propertyId || '',
        loanAmount: this.formatINR(this.loan.loanAmount),
        lenderName: this.loan.lenderName || '',
        interestRate: this.loan.interestRate || '',
        tenure: this.loan.tenure || '',
        totalInterest: this.formatINR(this.loan.totalInterest),
        totalRepayment: this.formatINR(this.loan.totalRepayment),
        loanDate: this.loan.loanDate ? this.loan.loanDate.substring(0, 10) : this.getTodayString(),
        remarks: this.loan.remarks || ''
      });
    }
  }
  @Input() loan: any;
  @Input() loanId?: number;
  @Output() closeModal = new EventEmitter<void>();
  @Output() success = new EventEmitter<any>();

  loanForm!: FormGroup;
  properties: Property[] = [];
  isViewMode = false;

  constructor(private fb: FormBuilder, private propertyService: PropertyService) {
    this.initForm();
    this.fetchProperties();
    this.setupInterestCalculation();
  }

  formatINR(value: any): string {
    if (value === null || value === undefined || value === '') return '';
    let num = Number(value.toString().replace(/,/g, ''));
    if (isNaN(num)) return value;
    return num.toLocaleString('en-IN', { maximumFractionDigits: 2 });
  }

  patchFormattedField(field: string) {
    const ctrl = this.loanForm.get(field);
    if (ctrl) {
      let raw = ctrl.value;
      if (raw === null || raw === undefined || raw === '' || raw === 0 || raw === '0' || raw === '0.00') {
        ctrl.setValue('0', { emitEvent: false });
      } else {
        ctrl.setValue(this.formatINR(raw), { emitEvent: false });
      }
    }
  }

  private initForm() {
    this.loanForm = this.fb.group({
      property: ['', Validators.required],
      loanAmount: ['', [Validators.required, Validators.min(1)]],
      lenderName: ['', Validators.required],
      interestRate: ['', [Validators.required, Validators.min(0)]],
      tenure: ['', [Validators.required, Validators.min(1)]],
      totalInterest: [{ value: '', disabled: true }],
      totalRepayment: [{ value: '', disabled: true }],
      loanDate: [this.getTodayString(), Validators.required],
      remarks: ['']
    });
  }

  private fetchProperties() {
    this.propertyService.getActiveProperties().subscribe({
      next: (props) => {
        this.properties = props || [];
      },
      error: (err) => {
        console.error('Failed to fetch properties:', err);
      }
    });
  }

  private setupInterestCalculation() {
    this.loanForm.valueChanges.subscribe(values => {
      this.calculateInterest(values);
    });
  }

  private calculateInterest(values: any) {
    const principle = parseFloat((values.loanAmount || '').toString().replace(/,/g, ''));
    const rate = parseFloat(values.interestRate);
    const months = parseInt(values.tenure, 10);
    const hasPrinciple = !isNaN(principle);
    const hasRate = !isNaN(rate);
    const hasMonths = !isNaN(months);

    if (hasPrinciple && hasRate && hasMonths) {
      const interestPerPeriod = principle * rate / 100;
      const totalInterest = interestPerPeriod * months;
      const total = principle + totalInterest;
      const formattedInterest = this.formatINR(totalInterest).replace(/\.00$/, '');
      const formattedTotal = this.formatINR(total).replace(/\.00$/, '');
      this.loanForm.get('totalInterest')?.setValue(formattedInterest, { emitEvent: false });
      this.loanForm.get('totalRepayment')?.setValue(formattedTotal, { emitEvent: false });
    } else if (!values.loanAmount && !values.interestRate && !values.tenure) {
      this.loanForm.get('totalInterest')?.setValue('', { emitEvent: false });
      this.loanForm.get('totalRepayment')?.setValue('', { emitEvent: false });
    }
  }

  private getTodayString(): string {
    const today = new Date();
    const yyyy = today.getFullYear();
    const mm = String(today.getMonth() + 1).padStart(2, '0');
    const dd = String(today.getDate()).padStart(2, '0');
    return `${yyyy}-${mm}-${dd}`;
  }

  closeModalClicked() {
    this.closeModal.emit();
  }

  onSubmit() {
    // If in view mode, just close the form without saving
    if (this.isViewMode) {
      this.closeModalClicked();
      return;
    }

    if (!this.loanForm.valid) {
      this.loanForm.markAllAsTouched();
      return;
    }

    const formValue = this.loanForm.getRawValue();
    const selectedProperty = this.properties.find(p => p.id === Number(formValue.property));
    const payload = {
      id: this.loan?.id || 0,
      propertyId: selectedProperty ? selectedProperty.id : 0,
      propertyName: selectedProperty ? selectedProperty.title : '',
      loanAmount: parseFloat((formValue.loanAmount || '').toString().replace(/,/g, '')),
      lenderName: formValue.lenderName,
      interestRate: parseFloat((formValue.interestRate || '').toString().replace(/,/g, '')),
      tenure: parseInt((formValue.tenure || '').toString().replace(/,/g, ''), 10),
      totalInterest: parseFloat((formValue.totalInterest || '').toString().replace(/,/g, '')),
      totalRepayment: parseFloat((formValue.totalRepayment || '').toString().replace(/,/g, '')),
      loanDate: formValue.loanDate ? new Date(formValue.loanDate).toISOString() : new Date().toISOString(),
      remarks: formValue.remarks,
      createdDate: new Date().toISOString(),
      createdBy: '',
      modifiedBy: '',
      modifiedDate: new Date().toISOString(),
      isDeleted: false,
      totalPaid: 0,
      outstanding: 0
    };

    if (this.loan && this.loan.id) {
      // Edit mode: update existing loan
      this.propertyService.updatePropertyLoan(this.loan.id, payload).subscribe({
        next: (res) => {
          this.success.emit(res);
          this.closeModal.emit();
        },
        error: (err) => {
          console.error('Failed to update loan:', err);
        }
      });
    } else {
      // Create mode: new loan
      this.propertyService.createPropertyLoan(payload).subscribe({
        next: (res) => {
          this.success.emit(res);
          this.closeModal.emit();
        },
        error: (err) => {
          console.error('Failed to save loan:', err);
        }
      });
    }
  }
}
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AgentService } from '../services/agent.service';
import { PlotService } from '../services/plot.service';
import { Agent } from '../models/agent.model';
import { CommonModule } from '@angular/common';
import { Plot } from '../models/plot.model';

@Component({
  selector: 'app-plot-form',
  templateUrl: './plot-form.component.html',
  styleUrls: ['./plot-form.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class PlotFormComponent {
  today: string = new Date().toISOString().slice(0, 10);
  public formatINR(value: any): string {
    if (value == null || value === '') return '';
    const num = typeof value === 'string' ? parseFloat(value.replace(/,/g, '')) : value;
    if (isNaN(num)) return '';
    return num.toLocaleString('en-IN', { maximumFractionDigits: 0 });
  }

  private addINRFormatting(controlName: string) {
    this.plotForm.get(controlName)?.valueChanges.subscribe((value) => {
      if (value !== null && value !== '') {
        const num = typeof value === 'string' ? parseFloat(value.replace(/,/g, '')) : value;
        if (!isNaN(num)) {
          const formatted = num.toLocaleString('en-IN', { maximumFractionDigits: 0 });
          if (value !== formatted) {
            this.plotForm.get(controlName)?.setValue(formatted, { emitEvent: false });
          }
        }
      }
    });
  }

  ngAfterViewInit() {
    window.addEventListener('keydown', this.handleEscKey);
  }

  ngOnDestroy() {
    window.removeEventListener('keydown', this.handleEscKey);
  }

  handleEscKey = (event: KeyboardEvent) => {
    if (event.key === 'Escape') {
      this.closeModalClicked();
    }
  }
  @Output() refreshPlots = new EventEmitter<void>();
  agents: Agent[] = [];
  @Input() plot: Plot | undefined;
  @Input() viewMode: boolean = false;
  @Output() closeModal = new EventEmitter<void>();
  @Output() success = new EventEmitter<void>();
  @Output() showMessage = new EventEmitter<string>();

  plotForm: FormGroup;
  saleId: number | null = null;

  constructor(private fb: FormBuilder, private plotService: PlotService, private agentService: AgentService) {
    const today = new Date().toISOString().slice(0, 10);
    this.plotForm = this.fb.group({
      saleId: [null],
      plotNumber: [{ value: '', disabled: true }, Validators.required],
      status: [{ value: '', disabled: true }, Validators.required],
      area: [0, [Validators.required, Validators.min(1)]],
      saleAmount: ['', [Validators.required, Validators.pattern(/^[\d,]+$/), this.saleAmountGreaterThanZeroValidator.bind(this)]],
      saleDate: [today, [Validators.required]],
      customerName: ['', Validators.required],
      customerPhone: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      customerEmail: ['', [Validators.required, Validators.email]],
      agentId: [null],
      brokerageAmount: ['', [Validators.pattern(/^[\d,]+$/)]],
      description: ['']
    }, { validators: this.brokerageRequiredIfAgentValidator.bind(this) });
  }
  // Validator: brokerageAmount required if agentId is selected
  brokerageRequiredIfAgentValidator(formGroup: FormGroup) {
    const agentId = formGroup.get('agentId')?.value;
    const brokerageAmount = formGroup.get('brokerageAmount')?.value;
    if (agentId) {
      const num = typeof brokerageAmount === 'string' ? parseFloat(brokerageAmount.replace(/,/g, '')) : brokerageAmount;
      if (!brokerageAmount || isNaN(num) || num <= 0) {
        return { brokerageRequired: true };
      }
    }
    return null;
  }

  // Validator: sale amount must be greater than 0
  saleAmountGreaterThanZeroValidator(control: any) {
    const value = control.value;
    if (value == null || value === '') return null;
    const num = typeof value === 'string' ? parseFloat(value.replace(/,/g, '')) : value;
    return num > 0 ? null : { min: true };
  }

  ngOnInit() {
    // Add INR formatting to relevant fields
    this.addINRFormatting('saleAmount');
    this.addINRFormatting('brokerageAmount');
    // Fetch agents for dropdown
    this.agentService.getAllAgents().subscribe({
      next: (agents) => {
        this.agents = agents;
      },
      error: () => {
        this.agents = [];
      }
    });
    if (this.plot) {
      const patch: any = { ...this.plot };
      if (patch.saleDate) {
        // Convert to YYYY-MM-DD if needed
        const d = new Date(patch.saleDate);
        if (!isNaN(d.getTime())) {
          patch.saleDate = d.toISOString().slice(0, 10);
          this.plotForm.patchValue({ saleDate: patch.saleDate });
        }
      }
      // Patch other fields except saleDate
      const { saleDate, ...otherFields } = patch;
      this.plotForm.patchValue(otherFields);
      if (patch.saleId) {
        this.saleId = patch.saleId;
      }
    }
    if (this.viewMode) {
      this.plotForm.disable();
    }
  }

  onSubmit() {
    if (!this.plotForm.valid) {
      this.plotForm.markAllAsTouched();
      this.showMessage.emit('Please fix validation errors before submitting.');
      return;
    }
    const saleData = this.plotForm.getRawValue();
    // Convert formatted amount fields to decimal
    if (typeof saleData.saleAmount === 'string') {
      saleData.saleAmount = parseFloat(saleData.saleAmount.replace(/,/g, ''));
    }
    if (typeof saleData.brokerageAmount === 'string') {
      saleData.brokerageAmount = parseFloat(saleData.brokerageAmount.replace(/,/g, ''));
    }
    // Convert agentId to integer if not empty
    if (saleData.agentId !== null && saleData.agentId !== '') {
      saleData.agentId = parseInt(saleData.agentId, 10);
    }
    else {
      saleData.agentId = null;
    }
    // Always include propertyId and plotId in saleData
    if (this.plot) {
      if (this.plot.propertyId) {
        saleData.propertyId = this.plot.propertyId;
      }
      if (this.plot.id) {
        saleData.plotId = this.plot.id;
      }
    }
    if (this.saleId) {
      // Edit sale
      this.plotService.editPlotSale(this.saleId, saleData).subscribe({
        next: () => {
          this.success.emit(saleData);
          this.showMessage.emit('Plot updated successfully');
          this.closeModal.emit();
          this.refreshPlots.emit();
        },
        error: () => alert('Failed to update sale.')
      });
    } else if (this.plot && this.plot.id) {
      // Add sale
      this.plotService.addPlotSale(this.plot.id, saleData).subscribe({
        next: () => {
          this.success.emit(saleData);
          this.showMessage.emit('Plot sale successful');
          this.closeModal.emit();
          this.refreshPlots.emit();
        },
        error: () => alert('Failed to add sale.')
      });
    }
  }

  closeModalClicked() {
    this.closeModal.emit();
  }
}

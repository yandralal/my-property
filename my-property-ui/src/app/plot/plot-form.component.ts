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
    this.plotForm = this.fb.group({
      saleId: [null],
      plotNumber: [{ value: '', disabled: true }, Validators.required],
      status: [{ value: '', disabled: true }, Validators.required],
      area: [0, [Validators.required, Validators.min(1)]],
      saleAmount: [0],
      saleDate: [''],
      customerName: [''],
      customerPhone: [''],
      customerEmail: [''],
      amountPaid: [{ value: 0, disabled: true }],
      amountBalance: [{ value: 0, disabled: true }],
      agentId: [null],
      brokerageAmount: [0],
      description: ['']
    });
  }

  ngOnInit() {
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
        }
      }
      this.plotForm.patchValue(patch);
      if (patch.saleId) {
        this.saleId = patch.saleId;
      }
    }
    if (this.viewMode) {
      this.plotForm.disable();
    }
  }

  onSubmit() {
    if (this.plotForm.valid) {
      const saleData = this.plotForm.getRawValue();
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
            this.showMessage.emit('Plot added successfully');
            this.closeModal.emit();
            this.refreshPlots.emit();
          },
          error: () => alert('Failed to add sale.')
        });
      }
    }
  }

  closeModalClicked() {
    this.closeModal.emit();
  }
}

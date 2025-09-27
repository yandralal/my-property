import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { PropertyService } from '../services/property.service';
import { AgentService } from '../services/agent.service';

@Component({
  selector: 'app-agent-transaction-form',
  templateUrl: './agent-transaction-form.component.html',
  styleUrls: ['./agent-transaction-form.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule]
})
export class AgentTransactionFormComponent implements OnInit {
  @Input() transaction: any;
  @Input() viewMode: boolean = false;
  @Input() agentList: any[] = [];
  @Input() agentId: string | number | undefined;
  @Output() closeModal = new EventEmitter<void>();
  @Output() success = new EventEmitter<string>();

  propertyList: any[] = [];
  activePropertyList: any[] = [];
  plotList: any[] = [];
  transactionTypes: string[] = ['Credit', 'Debit'];
  paymentModes: string[] = ['Cash', 'Cheque', 'Online', 'UPI'];
  isSubmitting = false;

  transactionForm: FormGroup;

  constructor(private fb: FormBuilder, private propertyService: PropertyService, private agentService: AgentService) {
    const today = new Date();
    const yyyy = today.getFullYear();
    const mm = String(today.getMonth() + 1).padStart(2, '0');
    const dd = String(today.getDate()).padStart(2, '0');
    const formattedToday = `${yyyy}-${mm}-${dd}`;
    this.transactionForm = this.fb.group({
      propertyId: ['', Validators.required],
      agentId: ['', Validators.required],
      plotId: ['', Validators.required],
      totalBrokerage: [{ value: '', disabled: true }],
      amountPaidTillDate: [{ value: '', disabled: true }],
      balanceAmount: [{ value: '', disabled: true }],
      transactionType: ['Debit', Validators.required],
      amount: ['', Validators.required],
      paymentMethod: ['Cash', Validators.required],
      referenceNumber: [''],
      notes: [''],
      transactionDate: [formattedToday, Validators.required]
    });
  }

  formatINR(value: any): string {
    if (value === null || value === undefined || value === '') return '';
    let num = Number(value.toString().replace(/,/g, ''));
    if (isNaN(num)) return value;
    return num.toLocaleString('en-IN', { maximumFractionDigits: 2 });
  }

  patchFormattedAmounts() {
    ['totalBrokerage', 'amountPaidTillDate', 'balanceAmount', 'amount'].forEach(field => {
      const ctrl = this.transactionForm.get(field);
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


  ngOnInit() {
    this.fetchActiveProperties();
    if (this.agentId) {
      this.transactionForm.patchValue({ agentId: this.agentId });
      this.onAgentChange();
    }
    this.subscribePropertyChanges();
    this.subscribeAgentChanges();
    this.subscribePlotChanges();
    this.setupBalanceCalculation();
    this.setupAmountFormatting();
  }

  private setupBalanceCalculation() {
    this.transactionForm.get('amount')?.valueChanges.subscribe((txnAmount: any) => {
      const totalBrokerageRaw = parseFloat((this.transactionForm.get('totalBrokerage')?.value || '0').toString().replace(/,/g, ''));
      const paidTillDateRaw = parseFloat((this.transactionForm.get('amountPaidTillDate')?.value || '0').toString().replace(/,/g, ''));
      const txnAmountRaw = parseFloat((txnAmount || '0').toString().replace(/,/g, ''));
      const remaining = totalBrokerageRaw - (paidTillDateRaw + txnAmountRaw);
      this.transactionForm.get('balanceAmount')?.setValue(remaining === 0 ? '0' : remaining);
    });
  }

  private fetchActiveProperties() {
    this.propertyService.getActiveProperties().subscribe({
      next: (props) => {
        this.activePropertyList = props || [];
      },
      error: () => {
        this.activePropertyList = [];
      }
    });
  }

  private subscribePropertyChanges() {
    this.transactionForm.get('propertyId')?.valueChanges.subscribe(propertyId => {
      if (propertyId) {
        this.agentService.getAgentByProperty(propertyId).subscribe({
          next: (agent) => {
            if (Array.isArray(agent)) {
              this.agentList = agent;
            } else if (agent) {
              this.agentList = [agent];
            } else {
              this.agentList = [];
            }
            if (agent) {
              this.transactionForm.patchValue({ agentId: agent.id });
            } else {
              this.transactionForm.patchValue({ agentId: '' });
            }
          },
          error: () => {
            this.agentList = [];
            this.transactionForm.patchValue({ agentId: '' });
          }
        });
      } else {
        this.agentList = [];
        this.transactionForm.patchValue({ agentId: '' });
      }
      this.plotList = [];
      this.transactionForm.patchValue({ plotId: '' });
    });
  }

  private subscribeAgentChanges() {
    this.transactionForm.get('agentId')?.valueChanges.subscribe(agentId => {
      const propertyId = this.transactionForm.get('propertyId')?.value;
      if (agentId && propertyId) {
        this.agentService.getPlotsByAgentProperty(agentId, propertyId).subscribe({
          next: (plots) => {
            this.plotList = plots || [];
          },
          error: () => {
            this.plotList = [];
          }
        });
      } else {
        this.plotList = [];
        this.transactionForm.patchValue({ plotId: '' });
      }
    });
  }

  private subscribePlotChanges() {
    this.transactionForm.get('plotId')?.valueChanges.subscribe(plotId => {
      const agentId = this.transactionForm.get('agentId')?.value;
      const propertyId = this.transactionForm.get('propertyId')?.value;
      if (agentId && propertyId && plotId) {
        this.agentService.getBrokerageSummary(agentId, propertyId, plotId).subscribe({
          next: (summary) => {
            this.transactionForm.patchValue({
              totalBrokerage: summary.totalBrokerage != null ? summary.totalBrokerage.toString() : '',
              amountPaidTillDate: summary.paidTillDate != null ? summary.paidTillDate.toString() : '0',
              balanceAmount: summary.outstandingBrokerage != null ? summary.outstandingBrokerage.toString() : '0'
            });
            this.patchFormattedAmounts();
          },
          error: () => {
            this.transactionForm.patchValue({
              totalBrokerage: '',
              amountPaidTillDate: '',
              balanceAmount: ''
            });
          }
        });
      } else {
        this.transactionForm.patchValue({
          totalBrokerage: '',
          amountPaidTillDate: '',
          balanceAmount: ''
        });
      }
    });
  }

  private setupAmountFormatting() {
    ['amount', 'amountPaidTillDate', 'balanceAmount'].forEach(field => {
      this.transactionForm.get(field)?.valueChanges.subscribe(() => {
        this.patchFormattedAmounts();
      });
    });
  }

  onAgentChange(event?: any) {
    const agentId = this.transactionForm.get('agentId')?.value;
    // Fetch plots for selected agent
    // Replace with actual API/service call
    if (agentId) {
      // Example: fetch plots for agent
      // this.plotService.getPlotsByAgent(agentId).subscribe(plots => this.plotList = plots);
      // For now, stub with empty array
      this.plotList = [];
    } else {
      this.plotList = [];
    }
    this.transactionForm.patchValue({ plotId: '' });
  }

  onSubmit() {
    if (this.transactionForm.valid) {
      this.isSubmitting = true;
      const raw = this.transactionForm.getRawValue();
      const payload = {
        agentId: parseInt(raw.agentId),
        plotId: parseInt(raw.plotId),
        propertyId: parseInt(raw.propertyId),
        transactionDate: raw.transactionDate,
        amount: parseFloat(raw.amount.toString().replace(/,/g, '')),
        paymentMethod: raw.paymentMethod,
        referenceNumber: raw.referenceNumber,
        transactionType: raw.transactionType,
        notes: raw.notes
      };
      this.agentService.createAgentTransaction(payload).subscribe({
        next: () => {
          this.isSubmitting = false;
          this.success.emit('Transaction saved successfully');
          this.closeModal.emit();
        },
        error: () => {
          this.isSubmitting = false;
          this.success.emit('Failed to save transaction');
        }
      });
    }
  }

  deleteTransaction(id: number) {
    this.agentService.deleteAgentTransaction(id).subscribe({
      next: () => {
        this.success.emit('Transaction deleted successfully');
        this.closeModal.emit();
      },
      error: () => {
        this.success.emit('Failed to delete transaction');
      }
    });
  }

  closeModalClicked() {
    this.closeModal.emit();
  }
}

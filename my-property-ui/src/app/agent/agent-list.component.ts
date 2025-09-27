import { Component, OnInit } from '@angular/core';
import { Output, EventEmitter } from '@angular/core';
import { Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgentService } from '../services/agent.service';
import { AgentTransactionsListComponent } from './agent-transactions-list.component';
import { AgentFormComponent } from './agent-form.component';
import { MessageBoxComponent } from '../shared/message-box.component';
import { ConfirmDialogComponent } from '../shared/confirm-dialog.component';
import { InrFormatPipe } from "../shared/inr-format.pipe";
@Component({
  selector: 'app-agent-list',
  templateUrl: './agent-list.component.html',
  styleUrls: ['./agent-list.component.css'],
  standalone: true,
  imports: [CommonModule, MessageBoxComponent, ConfirmDialogComponent, InrFormatPipe, AgentFormComponent]
})
export class AgentListComponent implements OnInit {
  confirmDeleteAgentVisible: boolean = false;
  agentToDelete: any = null;

  showAgentFormModal: boolean = false;

  @Output() selectAgent = new EventEmitter<any>();
  @Output() editAgent = new EventEmitter<any>();
  @Input() agents: any[] = [];
  agentTransactions: any[] = [];
  selectedAgentId: number | null = null;

  messageBoxVisible: boolean = false;
  messageText: string = '';

  editAgentData: any = null;

  showMessage(msg: string) {
    this.messageText = msg;
    this.messageBoxVisible = true;
    setTimeout(() => {
      this.messageBoxVisible = false;
    }, 1500);
  }

  constructor(private agentService: AgentService) {}

  ngOnInit() {
    this.agentService.getAllAgents().subscribe({
      next: (data) => {
        this.agents = data;
        if (this.agents.length > 0) {
          this.selectedAgentId = this.agents[0].id;
          this.fetchAgentTransactions(this.agents[0].id);
        }
      },
      error: (err) => {
        console.error('Failed to fetch agents', err);
        this.agents = [];
      }
    });
  }

  closeAgentFormModal() {
    this.showAgentFormModal = false;
  }

  fetchAgentTransactions(agentId: number) {
    this.agentService.getAgentTransactions(agentId).subscribe({
      next: (txns) => this.agentTransactions = txns,
      error: (err) => {
        console.error('Failed to fetch agent transactions', err);
        this.agentTransactions = [];
      }
    });
  }

  onSelectAgent(agent: any) {
    this.selectedAgentId = agent.id;
    this.fetchAgentTransactions(agent.id);
    this.selectAgent.emit(agent);
  }

  onManagePlots() {
    this.showMessage('Manage Plots clicked');
  }


  onViewAgent(agent: any) {
    if (!agent?.id) return;
    this.agentService.getAgentById(agent.id).subscribe({
      next: (data) => {
        this.showMessage('Agent Details: ' + data.name + ', ' + data.contact + ', ' + data.agency);
      },
      error: () => {
        this.showMessage('Failed to fetch agent details.');
      }
    });
  }

  onEditAgent(agent: any) {
    if (!agent?.id) return;
    this.selectedAgentId = agent.id;
    this.agentService.getAgentById(agent.id).subscribe({
      next: (data) => {
        this.editAgentData = data;
        this.showAgentFormModal = true;
        this.editAgent.emit(agent); // Emit the edit event
      },
      error: () => {
        this.showMessage('Failed to fetch agent details for edit.');
      }
    });
  }

  onDeleteAgent(agent: any) {
    if (!agent?.id) return;
    this.agentToDelete = agent;
    this.confirmDeleteAgentVisible = true;
  }

  onConfirmDeleteAgent() {
    if (!this.agentToDelete?.id) return;
    this.agentService.deleteAgent(this.agentToDelete.id).subscribe({
      next: () => {
        this.showMessage('Agent deleted successfully.');
        // Always refresh agent table after deletion
        this.agentService.getAllAgents().subscribe({
          next: (data) => {
            this.agents = data;
            if (this.agents.length > 0) {
              this.selectedAgentId = this.agents[0].id;
              this.fetchAgentTransactions(this.agents[0].id);
            } else {
              this.selectedAgentId = null;
              this.agentTransactions = [];
            }
          },
          error: () => {
            this.agents = [];
            this.selectedAgentId = null;
            this.agentTransactions = [];
          }
        });
        this.confirmDeleteAgentVisible = false;
        this.agentToDelete = null;
      },
      error: () => {
        this.showMessage('Failed to delete agent.');
        this.confirmDeleteAgentVisible = false;
        this.agentToDelete = null;
      }
    });
  }

  onCancelDeleteAgent() {
    this.confirmDeleteAgentVisible = false;
    this.agentToDelete = null;
  }

  onWhatsAppAgent(agent: any) {
    this.showMessage('WhatsApp Agent: ' + agent.contact);
  }

  onRegisterAgent() {
    this.showAgentFormModal = true;
  }

  onAgentFormSuccess(agent: any) {
    this.showAgentFormModal = false;
    this.editAgentData = null;
    this.agentService.getAllAgents().subscribe({
      next: (data) => {
        this.agents = data;
        if (this.agents.length > 0) {
          this.selectedAgentId = this.agents[0].id;
          this.fetchAgentTransactions(this.agents[0].id);
        } else {
          this.selectedAgentId = null;
          this.agentTransactions = [];
        }
      },
      error: () => {
        this.agents = [];
        this.selectedAgentId = null;
        this.agentTransactions = [];
      }
    });
  }
}
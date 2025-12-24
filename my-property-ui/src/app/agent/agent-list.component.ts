import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgentService } from '../services/agent.service';
import { MessageBoxComponent } from '../shared/message-box.component';
import { ConfirmDialogComponent } from '../shared/confirm-dialog.component';
import { InrFormatPipe } from '../shared/inr-format.pipe';
@Component({
  selector: 'app-agent-list',
  templateUrl: './agent-list.component.html',
  styleUrls: ['./agent-list.component.css'],
  standalone: true,
  imports: [CommonModule, MessageBoxComponent, ConfirmDialogComponent, InrFormatPipe]
})
export class AgentListComponent implements OnInit {
  @Input() agents: any[] = [];
  @Output() selectAgent = new EventEmitter<any>();
  @Output() viewAgent = new EventEmitter<any>();
  @Output() editAgent = new EventEmitter<any>();
  @Output() agentDeleted = new EventEmitter<void>();

  agentTransactions: any[] = [];
  selectedAgentId: number | null = null;
  showAgentFormModal = false;
  confirmDeleteAgentVisible = false;
  agentToDelete: any = null;
  editAgentData: any = null;
  messageBoxVisible = false;
  messageText = '';
  
  // Pagination
  currentPage = 1;
  pageSize = 10;
  
  get paginatedAgents(): any[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    return this.agents.slice(startIndex, endIndex);
  }
  
  get totalPages(): number {
    return Math.ceil(this.agents.length / this.pageSize);
  }
  
  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
    }
  }

  constructor(private agentService: AgentService) { }

  ngOnInit() {
    this.loadAgentsAndSelectFirst();
  }

  private loadAgentsAndSelectFirst() {
    this.agentService.getAllAgents().subscribe({
      next: (data) => {
        this.agents = data || [];
        if (this.agents.length > 0) {
          this.selectAndFetchTransactions(this.agents[0]);
        } else {
          this.selectedAgentId = null;
          this.agentTransactions = [];
        }
      },
      error: (err) => {
        console.error('Failed to fetch agents', err);
        this.agents = [];
        this.selectedAgentId = null;
        this.agentTransactions = [];
      }
    });
  }

  private selectAndFetchTransactions(agent: any) {
    this.selectedAgentId = agent.id;
    this.fetchAgentTransactions(agent.id);
    this.selectAgent.emit(agent);
  }

  fetchAgentTransactions(agentId: number) {
    this.agentService.getAgentTransactions(agentId).subscribe({
      next: (txns) => this.agentTransactions = txns || [],
      error: (err) => {
        console.error('Failed to fetch agent transactions', err);
        this.agentTransactions = [];
      }
    });
  }

  onSelectAgent(agent: any) {
    this.selectAndFetchTransactions(agent);
  }

  onViewAgent(agent: any) {
    this.viewAgent.emit(agent);
  }

  onEditAgent(agent: any) {
    if (!agent?.id) return;
    this.selectedAgentId = agent.id;
    this.agentService.getAgentById(agent.id).subscribe({
      next: (data) => {
        this.editAgentData = data;
        this.showAgentFormModal = true;
        this.editAgent.emit(agent);
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
        this.agentDeleted.emit();
        this.loadAgentsAndSelectFirst();
        // Adjust pagination if current page is now empty
        setTimeout(() => {
          if (this.currentPage > this.totalPages && this.totalPages > 0) {
            this.currentPage = this.totalPages;
          }
        }, 100);
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
    this.loadAgentsAndSelectFirst();
  }

  showMessage(msg: string) {
    this.messageText = msg;
    this.messageBoxVisible = true;
    setTimeout(() => {
      this.messageBoxVisible = false;
    }, 1500);
  }

}
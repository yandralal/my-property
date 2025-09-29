import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Property } from '../models/property.model';
import { Router } from '@angular/router';
import { PropertyService } from '../services/property.service';
import { PlotTransactionFormComponent } from '../plot/plot-transaction-form.component';
import { PlotService } from '../services/plot.service';
import { Plot } from '../models/plot.model';
import { MessageBoxComponent } from '../shared/message-box.component';
import { PropertyFormComponent } from '../property/property-form.component';
import { PropertyTransactionFormComponent } from '../property/property-transaction-form.component';
import { PlotFormComponent } from '../plot/plot-form.component';
import { ManagePlotsComponent } from '../manage-plots/manage-plots.component';
import { PropertyTransactionsListComponent } from '../property/property-transactions-list.component';
import { PlotTransactionsListComponent } from '../plot/plot-transactions-list.component';
import { ConfirmDialogComponent } from '../shared/confirm-dialog.component';
import { InrFormatPipe } from '../shared/inr-format.pipe';
import { AgentListComponent } from '../agent/agent-list.component';
import { AgentService } from '../services/agent.service';
import { AgentTransactionsListComponent } from '../agent/agent-transactions-list.component';
import { AgentFormComponent } from '../agent/agent-form.component';
import { AgentTransactionFormComponent } from '../agent/agent-transaction-form.component';
import { LoanListComponent } from '../loan/loan-list.component';
import { MiscListComponent } from '../misc/misc-list.component';
import { LoanTransactionsListComponent } from '../loan/loan-transactions-list.component';
import { LoanTransactionFormComponent } from '../loan/loan-transaction-form.component';
import { LoanFormComponent } from '../loan/loan-form.component';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    standalone: true,
    imports: [
        CommonModule,
        FormsModule,
        MessageBoxComponent,
        PropertyFormComponent,
        ManagePlotsComponent,
        PlotFormComponent,
        PropertyTransactionFormComponent,
        PropertyTransactionsListComponent,
        PlotTransactionsListComponent,
        ConfirmDialogComponent,
        PlotTransactionFormComponent,
        InrFormatPipe,
        AgentListComponent,
        AgentFormComponent,
        AgentTransactionsListComponent,
        AgentTransactionFormComponent,
        LoanListComponent,
        MiscListComponent,
        LoanTransactionsListComponent,
        LoanTransactionFormComponent,
        LoanFormComponent
    ]
})

export class HomeComponent implements OnInit {
    private loadLoansAndSelectFirst() {
            this.propertyService.getAllPropertyLoans().subscribe({
                next: (data) => {
                    this.loans = data || [];
                    if (this.loans.length > 0) {
                        this.selectedLoanId = this.loans[0].id;
                        this.onSelectLoan(this.loans[0]);
                        this.fetchLoanTransactions(this.loans[0].id);
                    } else {
                        this.selectedLoanId = null;
                        this.loanTransactions = [];
                    }
                },
                error: (err) => {
                    console.error('Failed to fetch loans', err);
                    this.loans = [];
                    this.selectedLoanId = null;
                    this.loanTransactions = [];
                }
            });
    }
    loans: any[] = [];

    onSelectLoan(loan: any) {
        this.selectedLoanId = loan.id;
        this.fetchLoanTransactions(loan.id);
    }

    fetchLoanTransactions(loanId: number) {
        this.propertyService.getPropertyLoanTransactions(loanId).subscribe({
            next: (txns) => {
                this.loanTransactions = txns || [];
            },
            error: () => {
                this.loanTransactions = [];
            }
        });
    }

    openEditLoanModal(loan: any) {
        this.editLoanData = loan;
        this.showLoanFormModal = true;
    }
    onAgentTransactionFormSuccess(msg: string) {
        this.closeAgentTransactionFormModal();
        this.showMessage(msg);
        if (this.selectedAgentId) {
            this.fetchAgentTransactions(this.selectedAgentId);
        }
        this.agentService.getAllAgents().subscribe({
            next: (agents) => {
                this.agents = agents || [];
            },
            error: () => {
                this.agents = [];
            }
        });
    }
    showAgentTransactionFormModal: boolean = false;

    onAddAgentTransaction() {
        this.showAgentTransactionFormModal = true;
    }

    onAddPropertyTransaction() {
        this.showPropertyTransactionFormModal = true;
    }

    onAddPlotTransaction() {
        this.showPlotTransactionFormModal = true;
    }

    closeAgentTransactionFormModal() {
        this.showAgentTransactionFormModal = false;
    }

    showLoansLandingPage: boolean = false;
    showMiscLandingPage: boolean = false;
    showAgentsLandingPage: boolean = false;
    showAgentFormModal: boolean = false;
    showLoanFormModal: boolean = false;

    openAgentFormModal() {
        this.showAgentFormModal = true;
    }
    
    openLoanFormModal() {
        this.editLoanData = null;
        this.showLoanFormModal = true;
    }

    closeLoanFormModal() {
        this.showLoanFormModal = false;
    }

    closeAgentFormModal() {
        this.showAgentFormModal = false;
    }
    
    onAgentFormSuccess(agent: any) {
        if (agent) {
            this.closeAgentFormModal();
            this.showMessage('Agent saved successfully.');
            this.agentService.getAllAgents().subscribe({
                next: (agents) => {
                    this.agents = agents || [];
                },
                error: () => {
                    this.agents = [];
                }
            });
        }
    }
    agents: any[] = [];

    loanTransactions: any[] = [];
    selectedPropertyLoanId: number | null = null;
    showLoanTransactionFormModal: boolean = false;
    selectedLoanTransactionToDelete: any = null;
    confirmDeleteLoanTransactionVisible: boolean = false;
    editLoanData: any = null;

    onAddLoanTransaction() {
        this.showLoanTransactionFormModal = true;
    }

    onRequestDeleteLoanTransaction(txn: any) {
        this.selectedLoanTransactionToDelete = txn;
        this.confirmDeleteLoanTransactionVisible = true;
    }

    closeLoanTransactionFormModal() {
        this.showLoanTransactionFormModal = false;
    }

    onLoanTransactionFormSuccess(event: any) {
        this.closeLoanTransactionFormModal();
    }

    onLoanFormSuccess(event: any) {
        this.closeLoanFormModal();
        this.fetchLoans();
    }

    onLoanDeleted(loanId: number) {
        this.selectedLoanId = null;
        this.fetchLoans();
    }

    onLoanEdited(loan: any) {
        this.openEditLoanModal(loan);
    }
    
    onSalePlot(plot: any) {
        if (plot && plot.id) {
            this.plotViewMode = false;
            this.plotService.getPlotById(plot.id).subscribe({
                next: (plotDetails) => {
                    this.selectedPlotDetails = plotDetails;
                    this.showPlotFormModal = true;
                },
                error: () => {
                    this.showPlotFormModal = true;
                }
            });
        }
    }
    showPlotTransactionFormModal: boolean = false;
    closePlotTransactionFormModal() {
        this.showPlotTransactionFormModal = false;
        this.selectedTransactionDetails = null;
    }
    // Success handler for plot transaction form
    onPlotTransactionFormSuccess(message: string) {
        this.closePlotTransactionFormModal();
        this.showMessage(message);
        this.refreshPlotTransactions();
        this.refreshPlots();
    }
    confirmDeletePlotTransactionVisible: boolean = false;
    plotTransactionToDelete: any = null;
    onRequestDeletePlotTransaction(txn: any) {
        this.plotTransactionToDelete = txn;
        this.confirmDeletePlotTransactionVisible = true;
    }

    onConfirmDeletePlotTransaction() {
        if (!this.plotTransactionToDelete || !this.plotTransactionToDelete.transactionId) return;
        this.showLoader(true);
        this.plotService.deletePlotTransaction(this.plotTransactionToDelete.transactionId).subscribe({
            next: () => {
                this.showLoader(false);
                this.showMessage('Plot transaction deleted successfully.');
                this.refreshPlotTransactions();
                this.refreshPlots();
                this.confirmDeletePlotTransactionVisible = false;
                this.plotTransactionToDelete = null;
            },
            error: () => {
                this.showLoader(false);
                this.showMessage('Failed to delete plot transaction.');
                this.confirmDeletePlotTransactionVisible = false;
                this.plotTransactionToDelete = null;
            }
        });
    }

    onCancelDeletePlotTransaction() {
        this.confirmDeletePlotTransactionVisible = false;
        this.plotTransactionToDelete = null;
    }

    refreshPlotTransactions() {
        if (this.selectedPlotId) {
            this.plotService.getPlotTransactions(this.selectedPlotId).subscribe({
                next: (txns) => {
                    this.plotTransactions = txns || [];
                },
                error: () => {
                    this.plotTransactions = [];
                }
            });
        }
        else {
            this.plotTransactions = [];
        }
    }
    confirmDeleteTransactionVisible: boolean = false;
    transactionToDelete: any = null;
    onRequestDeletePropertyTransaction(txn: any) {
        console.log('Delete transaction event received:', txn);
        this.transactionToDelete = txn;
        this.confirmDeleteTransactionVisible = true;
        console.log('confirmDeleteTransactionVisible:', this.confirmDeleteTransactionVisible);
    }

    onConfirmDeletePropertyTransaction() {
        if (!this.transactionToDelete || !this.transactionToDelete.transactionId) return;
        this.showLoader(true);
        this.propertyService.deletePropertyTransaction(this.transactionToDelete.transactionId).subscribe({
            next: () => {
                this.showLoader(false);
                this.showMessage('Transaction deleted successfully.');
                this.fetchTransactions();
                this.refreshProperties();
                this.confirmDeleteTransactionVisible = false;
                this.transactionToDelete = null;
            },
            error: () => {
                this.showLoader(false);
                this.showMessage('Failed to delete transaction.');
                this.confirmDeleteTransactionVisible = false;
                this.transactionToDelete = null;
            }
        });
    }

    onCancelDeletePropertyTransaction() {
        this.confirmDeleteTransactionVisible = false;
        this.transactionToDelete = null;
    }
    propertyTransactions: any[] = [];
    plotTransactions: any[] = [];
    activeTransactionTab: 'property' | 'plot' = 'property';
    confirmDeletePropertyVisible: boolean = false;
    propertyToDelete: Property | null = null;
    propertyList: any[] = [];
    loading: boolean = false;
    error: string = '';
    transactionModal = { visible: false, type: '' };
    transactionListModal = { visible: false, type: '' };
    selectedAgentId: number | null = null;

    agentTransactions: any[] = [];
    selectedLoanId = null;
    selectedMiscId = null;
    selectedTransactionDetails: any = null;
    transactionViewMode: 'view' | 'edit' = 'view';
    showPropertyTransactionFormModal: boolean = false;
    showDropdown = false;
    properties: Property[] = [];
    plots: Plot[] = [];
    selectedPropertyId: number | undefined = undefined;
    selectedPlotId: number | undefined = undefined;
    selectedPropertyDetails: any = null;
    isSidebarCollapsed = false;
    expandedMenu: string | null = null;
    userName: string = 'User';
    showPropertyFormModal = false;
    viewMode: boolean = false;
    showManagePlotsModal: boolean = false;
    showPlotFormModal: boolean = false;
    selectedPlotDetails: any = null;
    plotViewMode: boolean = false;
    loaderVisible: boolean = false;
    messageBoxVisible: boolean = false;
    messageText: string = '';
    confirmDeletePlotVisible: boolean = false;
    plotToDelete: Plot | null = null;
    confirmDeleteAgentTransactionVisible: boolean = false;
    selectedAgentTransactionToDelete: any = null;
    editAgentData: any = null;

    constructor(
        private router: Router,
        private propertyService: PropertyService,
        private plotService: PlotService,
        private agentService: AgentService
    ) { }

    onTransactionMenuClick(menu: string) {
        // Show transaction entry modal for selected type
        this.expandedMenu = 'transactions';
        if (menu === 'property') {
            this.showPropertyTransactionFormModal = true;
        } else if (menu === 'plot') {
            this.showPlotTransactionFormModal = true;
        } else {
            this.transactionModal.visible = true;
            this.transactionModal.type = menu;
            this.transactionListModal.visible = false;
        }
    }

    onReportMenuClick(menu: string) {
        // Show transaction list modal for selected type
        this.expandedMenu = 'reports';
        this.transactionListModal.visible = true;
        this.transactionListModal.type = menu;
    }

    ngOnInit(): void {
        // Fetch agents and select first agent
        this.agentService.getAllAgents().subscribe({
            next: (agents) => {
                this.agents = agents || [];
                if (agents && agents.length > 0) {
                    this.selectedAgentId = agents[0].id;
                    this.fetchAgentTransactions(agents[0].id);
                }
            },
            error: (err) => {
                console.error('Failed to fetch agents:', err);
            }
        });

        // Fetch loans from API
        this.fetchLoans();

        // Fetch properties and select first property
        this.propertyService.getActiveProperties().subscribe({
            next: (props) => {
                this.properties = props;
                this.propertyList = props;
                if (props.length > 0) {
                    this.selectedPropertyId = props[0].id;
                    this.plotService.getPlotsByPropertyId(props[0].id).subscribe({
                        next: (plots) => {
                            this.plots = plots;
                            if (plots.length > 0) {
                                this.selectedPlotDetails = plots[0];
                                this.selectedPlotId = plots[0].id;
                                this.onSelectPlot(plots[0]);
                            }
                        },
                        error: (err) => {
                            console.error('Failed to fetch plots:', err);
                            this.plots = [];
                        }
                    });
                    this.fetchTransactions();
                }
            },
            error: (err) => {
                console.error('Failed to fetch properties:', err);
            }
        });
    }

    fetchLoans() {
        this.propertyService.getAllPropertyLoans().subscribe({
            next: (loans) => {
                this.loans = loans || [];
                if (this.loans.length > 0) {
                    this.onSelectLoan(this.loans[0]);
                } else {
                    this.selectedLoanId = null;
                    this.loanTransactions = [];
                }
            },
            error: (err) => {
                console.error('Failed to fetch loans:', err);
                this.loans = [];
                this.selectedLoanId = null;
                this.loanTransactions = [];
            }
        });
    }
    // Fetch agent transactions for selected agent
    fetchAgentTransactions(agentId: number) {
        this.agentService.getAgentTransactions(agentId).subscribe({
            next: (txns) => {
                this.agentTransactions = txns || [];
            },
            error: () => {
                this.agentTransactions = [];
            }
        });
    }

    // Change selected agent and fetch transactions
    onSelectAgent(agent: any) {
        this.selectedAgentId = agent.id;
        this.fetchAgentTransactions(agent.id);
    }
    
    ngOnChanges(changes: any): void {
        if (changes.transactionListModal && changes.transactionListModal.currentValue.visible) {
            this.fetchTransactions();
        }
    }

    toggleMenu(menu: string) {
        this.expandedMenu = this.expandedMenu === menu ? null : menu;
    }

    onRegisterProperty() {
        this.selectedPropertyDetails = null;
        this.showPropertyFormModal = true;
    }

    showLoader(show: boolean) {
        this.loaderVisible = show;
    }

    showMessage(msg: string) {
        this.messageText = msg;
        this.messageBoxVisible = true;
    }

    closeMessageBox() {
        this.messageBoxVisible = false;
    }

    onMenuClick(menu: string) {
        // Only show landing pages for agent, loan, or misc
        if (menu === 'agent') {
            this.showAgentsLandingPage = true;
            this.showLoansLandingPage = false;
            this.showMiscLandingPage = false;
        } else if (menu === 'loan') {
            this.showAgentsLandingPage = false;
            this.showLoansLandingPage = true;
            this.showMiscLandingPage = false;
            this.fetchLoans(); 
        } else if (menu === 'misc') {
            this.showAgentsLandingPage = false;
            this.showLoansLandingPage = false;
            this.showMiscLandingPage = true;
        } else {
            this.showAgentsLandingPage = false;
            this.showLoansLandingPage = false;
            this.showMiscLandingPage = false;
        }

        // Preserve existing sale logic
        if (menu === 'sale') {
            if (this.selectedPlotDetails && this.selectedPlotDetails.id) {
                // Only delete sale if user confirms, otherwise just open modal
                if (this.selectedPlotDetails.saleId && this.selectedPlotDetails.deleteRequested) {
                    this.showLoader(true);
                    this.plotService.deletePlotSale(this.selectedPlotDetails.saleId).subscribe({
                        next: () => {
                            this.showLoader(false);
                            this.showMessage('Sale deleted successfully.');
                            this.plotService.getPlotById(this.selectedPlotDetails.id).subscribe({
                                next: (plotDetails) => {
                                    this.selectedPlotDetails = plotDetails;
                                    this.showPlotFormModal = true;
                                },
                                error: (err) => {
                                    this.showPlotFormModal = true;
                                }
                            });
                        },
                        error: () => {
                            this.showLoader(false);
                            this.showMessage('Failed to delete sale.');
                            this.showPlotFormModal = true;
                        }
                    });
                } else {
                    this.plotViewMode = false;
                    this.plotService.getPlotById(this.selectedPlotDetails.id).subscribe({
                        next: (plotDetails) => {
                            this.selectedPlotDetails = plotDetails;
                            this.showPlotFormModal = true;
                        },
                        error: (err) => {
                            this.showPlotFormModal = true;
                        }
                    });
                }
            } else {
                this.selectedPlotDetails = null;
                this.plotViewMode = false;
                this.showPlotFormModal = true;
            }
        }
    }

    logout() {
        this.router.navigate(['/login']);
    }

    onViewProperty(property: Property) {
        this.selectedPropertyId = property.id;
        this.viewMode = true;
        this.propertyService.getPropertyById(property.id).subscribe({
            next: (propDetails) => {
                this.selectedPropertyDetails = propDetails;
                this.showPropertyFormModal = true;
            },
            error: (err) => {
                console.error('Failed to fetch property details:', err);
                this.plots = [];
            }
        });
    }

    onEditProperty(property: Property) {
        this.selectedPropertyId = property.id;
        this.viewMode = false;
        this.propertyService.getPropertyById(property.id).subscribe({
            next: (propDetails) => {
                this.selectedPropertyDetails = propDetails;
                this.showPropertyFormModal = true;
            },
            error: (err) => {
                console.error('Failed to fetch property details:', err);
                this.showMessage('Failed to load property details.');
            }
        });
    }

    onDeleteProperty(property: Property) {
        if (!property || !property.id) return;
        this.propertyToDelete = property;
        this.confirmDeletePropertyVisible = true;
    }

    onConfirmDeleteProperty() {
        if (!this.propertyToDelete || !this.propertyToDelete.id) return;
        this.showLoader(true);
        this.propertyService.deleteProperty(this.propertyToDelete.id).subscribe({
            next: () => {
                this.showLoader(false);
                this.showMessage('Property deleted successfully.');
                this.propertyService.getActiveProperties().subscribe({
                    next: (props) => {
                        this.properties = props;
                        this.propertyList = props;
                        if (props.length > 0) {
                            this.selectedPropertyId = props[0].id;
                            this.plotService.getPlotsByPropertyId(props[0].id).subscribe({
                                next: (plots) => {
                                    this.plots = plots;
                                    this.selectedPlotDetails = plots.length > 0 ? plots[0] : null;
                                },
                                error: () => {
                                    this.plots = [];
                                    this.selectedPlotDetails = null;
                                }
                            });
                        } else {
                            this.selectedPropertyId = undefined;
                            this.plots = [];
                            this.selectedPlotDetails = null;
                        }
                    },
                    error: () => {
                        this.properties = [];
                        this.propertyList = [];
                        this.selectedPropertyId = undefined;
                        this.plots = [];
                        this.selectedPlotDetails = null;
                    }
                });
                this.confirmDeletePropertyVisible = false;
                this.propertyToDelete = null;
            },
            error: () => {
                this.showLoader(false);
                this.showMessage('Failed to delete property.');
                this.confirmDeletePropertyVisible = false;
                this.propertyToDelete = null;
            }
        });
    }

    onCancelDeleteProperty() {
        this.confirmDeletePropertyVisible = false;
        this.propertyToDelete = null;
    }

    onWhatsAppProperty(property: Property) {
        console.log('WhatsApp property', property);
    }

    onViewPlot(plot: Plot) {
        this.plotViewMode = true;
        this.plotService.getPlotById(plot.id).subscribe({
            next: (plotDetails) => {
                this.selectedPlotDetails = plotDetails;
                this.showPlotFormModal = true;
            },
            error: (err) => {
                this.selectedPlotDetails = plot;
                this.showPlotFormModal = true;
            }
        });
    }

    onEditPlot(plot: Plot) {
        if ((plot.saleId && plot.saleId > 0) || plot.hasSale) {
            // Edit existing sale
            this.plotViewMode = false;
            this.plotService.getPlotById(plot.id).subscribe({
                next: (plotDetails) => {
                    this.selectedPlotDetails = plotDetails;
                    this.showPlotFormModal = true;
                },
                error: (err) => {
                    this.selectedPlotDetails = plot;
                    this.showPlotFormModal = true;
                }
            });
        } else {
            // Add new sale entry
            this.selectedPlotDetails = plot;
            this.plotViewMode = false;
            this.showPlotFormModal = true;
        }
    }

    onDeletePlot(plot: Plot) {
        if (!plot || !plot.id) return;
        this.showLoader(true);
        this.plotService.deletePlot(plot.id, this.selectedPropertyId!).subscribe({
            next: () => {
                this.showLoader(false);
                this.showMessage('Plot deleted successfully.');
                this.plotService.getPlotsByPropertyId(this.selectedPropertyId!).subscribe({
                    next: (plots) => {
                        this.plots = plots;
                        this.selectedPlotDetails = plots.length > 0 ? plots[0] : null;
                    },
                    error: () => {
                        this.plots = [];
                        this.selectedPlotDetails = null;
                    }
                });
            },
            error: () => {
                this.showLoader(false);
                this.showMessage('Failed to delete plot.');
            }
        });
    }

    onWhatsAppPlot(plot: Plot) {
        console.log('WhatsApp plot', plot);
    }

    testPropertyFormNavigation() {
        this.router.navigate(['/property-form']);
    }

    onPropertyFormSuccess(msg: string) {
        this.closePropertyFormModal();
        this.propertyService.getActiveProperties().subscribe({
            next: (props) => {
                this.properties = props;
            },
            error: (err) => {
                console.error('Failed to fetch properties:', err);
            }
        });
        this.showMessage(msg);
    }

    onSelectProperty(property: Property) {
        this.selectedPropertyId = property.id;
        this.refreshPlots();
        this.refreshPlotTransactions();
        this.fetchTransactions();
    }

    onSelectPlot(plot: Plot) {
        this.selectedPlotDetails = plot;
        this.plotTransactions = [];
        if (plot && plot.id) {
            this.selectedPlotId = plot.id;
            this.plotService.getPlotTransactions(plot.id).subscribe({
                next: (txns) => {
                    this.plotTransactions = txns || [];
                },
                error: (err) => {
                    this.plotTransactions = [];
                }
            });
        }
    }

    closePlotFormModal() {
        this.showPlotFormModal = false;
        this.selectedPlotDetails = null;
    }

    showConfirmDeletePlot(plot: Plot) {
        if ((plot.saleAmount && plot.saleAmount !== 0) || (plot.amountBalance && plot.amountBalance !== 0)) {
            this.showMessage('Cannot delete this plot because it has a sale record or outstanding balance.');
            return;
        }
        this.plotToDelete = plot;
        this.confirmDeletePlotVisible = true;
    }

    onConfirmDeletePlot() {
        if (this.plotToDelete) {
            this.onDeletePlot(this.plotToDelete);
        }
        this.confirmDeletePlotVisible = false;
        this.plotToDelete = null;
    }

    onCancelDeletePlot() {
        this.confirmDeletePlotVisible = false;
        this.plotToDelete = null;
    }

    openPropertyTransactionModal(propertyId: number, viewMode: boolean = false) {
        this.propertyService.getPropertyTransactionInfo(propertyId).subscribe({
            next: (info) => {
                this.selectedTransactionDetails = info;
                this.selectedPropertyId = propertyId;
                this.transactionViewMode = viewMode ? 'view' : 'edit';
                this.transactionModal.visible = true;
                this.transactionModal.type = 'property';
            },
            error: (err) => {
                this.selectedTransactionDetails = null;
                this.selectedPropertyId = propertyId;
                this.transactionViewMode = viewMode ? 'view' : 'edit';
                this.transactionModal.visible = true;
                this.transactionModal.type = 'property';
            }
        });
    }

    getTransactionFormModalClass(): string {
        switch (this.transactionModal.type) {
            case 'property': return 'property-transaction-form-modal';
            case 'plot': return 'plot-transaction-form-modal';
            case 'agent': return 'agent-transaction-form-modal';
            case 'loan': return 'loan-transaction-form-modal';
            case 'misc': return 'misc-transaction-form-modal';
            default: return 'transaction-form-modal';
        }
    }

    onTransactionFormSuccess(msg: string) {
        this.closeTransactionFormModal();
        this.showMessage(msg);
        // Optionally refresh transactions or property list here
    }

    closeTransactionFormModal() {
        this.transactionModal.visible = false;
        this.selectedTransactionDetails = null;
        this.transactionViewMode = 'view';
    }

    getTransactionListModalClass(): string {
        switch (this.transactionListModal.type) {
            case 'property': return 'property-transactions-list-modal';
            case 'plot': return 'plot-transactions-list-modal';
            case 'agent': return 'agent-transactions-list-modal';
            case 'loan': return 'loan-transactions-list-modal';
            case 'misc': return 'misc-transactions-list-modal';
            default: return 'property-transactions-list-modal';
        }
    }

    getPropertyListForTransaction() {
        return this.properties.map(p => ({
            id: p.id,
            title: p.title,
            selected: p.id === this.selectedPropertyId
        }));
    }

    onManagePlots() {
        this.showManagePlotsModal = true;
    }

    closeManagePlotsModal() {
        this.showManagePlotsModal = false;
        this.refreshPlots();
    }

    closePropertyFormModal() {
        this.showPropertyFormModal = false;
        this.selectedPropertyDetails = null;
    }

    refreshPlots() {
        if (this.selectedPropertyId) {
            this.plotService.getPlotsByPropertyId(this.selectedPropertyId).subscribe({
                next: (plots) => {
                    this.plots = plots;
                    this.selectedPlotDetails = plots.length > 0 ? plots[0] : null;
                    if (plots.length > 0) {
                        this.onSelectPlot(plots[0]);
                    }
                    else {
                        this.selectedPlotId = undefined;
                        this.plotTransactions = [];
                    }
                },
                error: () => {
                    this.plots = [];
                    this.selectedPlotDetails = null;
                }
            });
        }
    }

    fetchTransactions() {
        this.loading = true;
        this.error = '';
        this.propertyService.getPropertyTransactions(this.selectedPropertyId || 0).subscribe({
            next: (txns) => {
                this.propertyTransactions = txns || [];
                this.loading = false;
            },
            error: () => {
                this.propertyTransactions = [];
                this.error = 'Failed to load transactions.';
                this.loading = false;
            }
        });
    }

    closePropertyTransactionFormModal() {
        this.showPropertyTransactionFormModal = false;
        this.selectedTransactionDetails = null;
    }

    // Success handler for transaction form
    onPropertyTransactionFormSuccess(message: string) {
        this.closePropertyTransactionFormModal();
        this.showMessage(message);
        this.fetchTransactions();
        this.refreshProperties();
    }

    // Refresh property transactions list
    refreshPropertyTransactions() {
        if (this.selectedPropertyId) {
            this.propertyService.getPropertyTransactions(this.selectedPropertyId).subscribe({
                next: (txns) => {
                    this.propertyTransactions = txns;
                },
                error: () => {
                    this.propertyTransactions = [];
                }
            });
        }
    }

    onRequestDeleteAgentTransaction(txn: any) {
        // Show confirmation dialog and store transaction to delete
        this.selectedAgentTransactionToDelete = txn;
        this.confirmDeleteAgentTransactionVisible = true;
    }

    onConfirmDeleteAgentTransaction() {
        if (this.selectedAgentTransactionToDelete && this.selectedAgentTransactionToDelete.transactionId) {
            this.agentService.deleteAgentTransaction(this.selectedAgentTransactionToDelete.transactionId).subscribe({
                next: () => {
                    this.showMessage('Agent transaction deleted successfully.');
                    this.fetchAgentTransactions(this.selectedAgentId ?? 0);
                    this.agentService.getAllAgents().subscribe({
                        next: (agents) => {
                            this.agents = agents || [];
                        },
                        error: () => {
                            this.agents = [];
                        }
                    });
                },
                error: () => {
                    this.showMessage('Failed to delete agent transaction.');
                }
            });
        }
        this.confirmDeleteAgentTransactionVisible = false;
        this.selectedAgentTransactionToDelete = null;
    }

    onCancelDeleteAgentTransaction() {
        this.confirmDeleteAgentTransactionVisible = false;
        this.selectedAgentTransactionToDelete = null;
    }

    openEditAgentModal(agent: any) {
        this.editAgentData = agent;
        this.showAgentFormModal = true;
    }

    refreshProperties() {
        this.propertyService.getActiveProperties().subscribe({
            next: (properties) => {
                this.properties = properties || [];
            },
            error: () => {
                this.properties = [];
            }
        });
    }
}
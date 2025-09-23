import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Property } from '../models/property.model';
import { Router } from '@angular/router';
import { PropertyService } from '../services/property.service';
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
        ConfirmDialogComponent
    ]
})

export class HomeComponent implements OnInit {
    propertyTransactions: any[] = [];
    plotTransactions: any[] = [];
    activeTransactionTab: 'property' | 'plot' = 'property';
    onOpenPropertyTransaction(txn: any, viewMode: boolean) {
        this.selectedTransactionDetails = txn;
        this.transactionViewMode = viewMode ? 'view' : 'edit';
        this.transactionModal.visible = true;
        this.transactionModal.type = 'property';
    }
    confirmDeletePropertyVisible: boolean = false;
    propertyToDelete: Property | null = null;
    onRefreshPlotsFromModal(propertyId: number) {
        this.selectedPropertyId = propertyId;
        this.refreshPlots();
    }
    // For property transactions table
    propertyList: any[] = [];
    loading: boolean = false;
    error: string = '';

    // Property transactions table actions
    onViewTransaction(txn: any) {
        this.onOpenPropertyTransaction(txn, true);
    }

    onEditTransaction(txn: any) {
        this.onOpenPropertyTransaction(txn, false);
    }

    onDeleteTransaction(txn: any) {
        // Example: Show confirm dialog or log
        console.log('Delete transaction', txn);
    }

    closeModalClicked() {
        // Example: Hide modal or log
        console.log('Close property transactions modal');
    }

    // Always load transactions for selected property
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

    transactionModal = { visible: false, type: '' };
    transactionListModal = { visible: false, type: '' };
    selectedAgentId = null;
    selectedLoanId = null;
    selectedMiscId = null;
    selectedTransactionDetails: any = null;
    transactionViewMode: 'view' | 'edit' = 'view';
    showPropertyTransactionFormModal: boolean = false;

    onOpenPropertyTransactionForm(txn: any, mode: 'view' | 'edit') {
        this.selectedTransactionDetails = txn;
        this.transactionViewMode = mode;
        this.showPropertyTransactionFormModal = true;
    }

    closePropertyTransactionFormModal() {
        this.showPropertyTransactionFormModal = false;
        this.selectedTransactionDetails = null;
    }

    onPropertyTransactionFormSuccess(event: string) {
        this.closePropertyTransactionFormModal();
        this.fetchTransactions();
    }
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

    constructor(
        private router: Router,
        private propertyService: PropertyService,
        private plotService: PlotService
    ) { }

    onTransactionMenuClick(menu: string) {
        // Show transaction entry modal for selected type
        this.expandedMenu = 'transactions';
        if (menu === 'property') {
            this.showPropertyTransactionFormModal = true;
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
        // Determine which parent menu to keep expanded
        if ([
            'sale', 'agents', 'loans', 'miscDetails'
        ].includes(menu)) {
            this.expandedMenu = 'operations';
        } else if ([
            'viewReports'
        ].includes(menu)) {
            this.expandedMenu = 'reports';
        } else if ([
            'sendMessages'
        ].includes(menu)) {
            this.expandedMenu = 'notifications';
        } else if ([
            'help', 'about', 'customizeBackground', 'downloadGuide'
        ].includes(menu)) {
            this.expandedMenu = 'help';
        } else if ([
            'manageProperty', 'managePlots'
        ].includes(menu)) {
            this.expandedMenu = 'file';
        }
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
        // TODO: Implement other menu actions
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
        this.plotService.getPlotsByPropertyId(property.id).subscribe({
            next: (plots) => {
                this.plots = plots;
                if (plots.length > 0) {
                    this.selectedPlotDetails = plots[0];
                } else {
                    this.selectedPlotDetails = null;
                }
            },
            error: (err) => {
                console.error('Failed to fetch plots:', err);
                this.plots = [];
                this.selectedPlotDetails = null;
            }
        });
        this.fetchTransactions();
    }

    onSelectPlot(plot: Plot) {
            this.selectedPlotDetails = plot;
            this.plotTransactions = [];
            if (plot && plot.id) {
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
            default: return 'property-transaction-form-modal';
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
                },
                error: () => {
                    this.plots = [];
                    this.selectedPlotDetails = null;
                }
            });
        }
    }
}
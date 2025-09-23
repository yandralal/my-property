import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { PlotService } from '../services/plot.service';
import { PropertyService } from '../services/property.service'; 
import { FormControl } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { Plot } from '../models/plot.model'; 
import { ConfirmDialogComponent } from '../shared/confirm-dialog.component';
import { LoaderComponent } from '../shared/loader.component';
import { MessageBoxComponent } from '../shared/message-box.component';

@Component({
  selector: 'app-manage-plots',
  templateUrl: './manage-plots.component.html',
  styleUrls: ['./manage-plots.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    ConfirmDialogComponent,
    LoaderComponent,
    MessageBoxComponent
]
})
export class ManagePlotsComponent implements OnInit {
  refreshTable() {
    this.onPropertyChange();
  }
  @Output() refreshPlots = new EventEmitter<number>();
  @Output() closeModal = new EventEmitter<void>();
  properties: { id: number; title: string }[] = [];
  selectedPropertyId = new FormControl<number | null>(null);
  plotCount = new FormControl<number>(1);
  plots: Plot[] = [];
  editIndex: number | null = null;
  editingPlot: any;
  selectAll: boolean = false;
  showConfirmDialog: boolean = false;
  confirmDialogTitle: string = '';
  confirmDialogMessage: string = '';
  confirmDeleteMode: 'single' | 'bulk' = 'single';
  plotToDelete: Plot | null = null;
  plotsToDelete: Plot[] = [];
  showAddMultiple: boolean = false;
  multiplePlotCount: number = 1;
  loaderVisible = false;
  messageBoxVisible = false;
  messageText = '';

  constructor(
    private plotService: PlotService,
    private propertyService: PropertyService 
  ) {}

  ngOnInit() {
    // Fetch property list from your API
    this.propertyService.getActiveProperties().subscribe({
      next: (props) => {
        this.properties = props.map((p: any) => ({
          id: p.id,
          title: p.title
        }));
      },
      error: (err) => {
        console.error('Failed to fetch properties:', err);
      }
    });
  }

  // Ensure each plot has a 'selected' property
  onPropertyChange() {
    const propertyId = this.selectedPropertyId.value;
    if (propertyId) {
      this.plotService.getBasicPlotsByPropertyId(propertyId).subscribe((data: Plot[]) => {
        this.plots = data.map(plot => ({ ...plot, selected: false }));
        this.selectAll = false;
        this.showAddMultiple = false;
      });
    } else {
      this.plots = [];
      this.selectAll = false;
      this.showAddMultiple = false;
    }
  }

  toggleSelectAll() {
    this.plots.forEach(plot => plot.selected = this.selectAll);
  }

  onPlotSelectChange() {
    this.selectAll = this.plots.length > 0 && this.plots.every(plot => plot.selected);
  }

  hasSelectedPlots(): boolean {
    return this.plots.some(plot => plot.selected);
  }

  // Override deletePlot to show modal
  deletePlot(plot: Plot) {
    if (plot.status && plot.status.toLowerCase() === 'booked') {
      this.showMessage('Cannot delete a plot with status Booked.');
      return;
    }
    // Always show confirmation dialog for valid delete
    this.confirmDialogTitle = 'Delete Plot';
    this.confirmDialogMessage = 'Are you sure you want to delete this plot?';
    this.confirmDeleteMode = 'single';
    this.plotToDelete = plot;
    this.showConfirmDialog = true;
  }

  // Override deleteSelectedPlots to show modal
  deleteSelectedPlots() {
    if (!this.selectedPropertyId.value) {
      this.showMessage('Please select a property before deleting plots.');
      return;
    }
    const selectedPlots = this.plots.filter(plot => plot.selected);
    if (selectedPlots.length === 0) {
      this.showMessage('Please select one or more plots to delete.');
      return;
    }
    const nonDeletable = selectedPlots.filter(plot => plot.status && plot.status.toLowerCase() === 'booked');
    if (nonDeletable.length > 0) {
      this.showMessage('Cannot delete one or more plots with status Booked.');
      return;
    }
    // Always show confirmation dialog for valid bulk delete
    this.confirmDialogTitle = 'Delete Selected Plots';
    this.confirmDialogMessage = `Are you sure you want to delete ${selectedPlots.length} selected plot(s)?`;
    this.confirmDeleteMode = 'bulk';
    this.plotsToDelete = selectedPlots;
    this.showConfirmDialog = true;
  }

  onConfirmDelete() {
    if (this.confirmDeleteMode === 'single' && this.plotToDelete) {
      this.showLoader(true);
      this.plotService.deletePlot(this.plotToDelete.id, this.selectedPropertyId.value!).subscribe({
        next: () => {
          this.showLoader(false);
          this.showMessage('Plot deleted successfully.');
           this.refreshTable();
           this.refreshPlots.emit(this.selectedPropertyId.value!);
          this.showConfirmDialog = false;
          this.plotToDelete = null;
        },
        error: (err) => {
          this.showLoader(false);
          this.showMessage('Failed to delete plot.');
          this.showConfirmDialog = false;
        }
      });
    } else if (this.confirmDeleteMode === 'bulk' && this.plotsToDelete.length > 0) {
        const plotIds = this.plotsToDelete.map(plot => Number(plot.id)).filter(id => !isNaN(id));
        this.showLoader(true);
        this.plotService.bulkDeletePlots(plotIds).subscribe({
          next: () => {
            this.showLoader(false);
            this.showMessage('Selected plots deleted successfully.');
             this.refreshPlots.emit();
            this.showConfirmDialog = false;
            this.plotsToDelete = [];
          },
          error: (err) => {
            this.showLoader(false);
            this.showMessage('Failed to delete selected plots.');
            this.showConfirmDialog = false;
          }
        });
    }
  }

  onCancelDelete() {
    this.showConfirmDialog = false;
    this.plotToDelete = null;
    this.plotsToDelete = [];
  }

  onPlotCountChange() {
    const propertyId = this.selectedPropertyId.value;
    const count = this.plotCount.value ?? 0;
    if (propertyId && count > 0) {
    }
  }

  addPlot() {
    if (!this.selectedPropertyId.value) {
      this.showMessage('Please select a property before adding a plot.');
      return;
    }
    let maxPlotNumber = 0;
    if (this.plots.length > 0) {
      const numbers = this.plots.map(p => {
        const match = typeof p.plotNumber === 'string' ? p.plotNumber.match(/Plot-(\d+)/) : null;
        return match ? parseInt(match[1], 10) : 0;
      }).filter(n => !isNaN(n));
      maxPlotNumber = numbers.length ? Math.max(...numbers) : 0;
    }
    const now = new Date().toISOString();
    const newPlot: Plot = {
      id: 0,
      plotNumber: `Plot-${maxPlotNumber + 1}`,
      status: 'Available',
      area: 0,
      saleAmount: 0,
      customerName: '',
      customerPhone: '',
      customerEmail: '',
      hasSale: false,
      saleId: 0,
      amountPaid: 0,
      amountBalance: 0,
      selected: false,
      addedAt: now,
      propertyId: this.selectedPropertyId.value!
    };
    this.showAddMultiple = false;
    this.showLoader(true);
      this.plotService.createPlot(newPlot, this.selectedPropertyId.value!).subscribe({
        next: (createdPlot) => {
          this.showLoader(false);
          this.showMessage('Plot added successfully.');
          this.refreshTable();
          this.refreshPlots.emit(this.selectedPropertyId.value!);
        },
        error: (err) => {
          this.showLoader(false);
          this.showMessage('Failed to add plot.');
        }
      });
  }

  addMultiplePlots() {
    if (!this.selectedPropertyId.value) {
      this.showMessage('Please select a property before adding plots.');
      return;
    }
    const count = this.multiplePlotCount;
    if (!count || count < 1) {
      this.showMessage('Please enter a valid number of plots to add.');
      return;
    }
    let maxPlotNumber = 0;
    if (this.plots.length > 0) {
      const numbers = this.plots.map(p => {
        const match = typeof p.plotNumber === 'string' ? p.plotNumber.match(/Plot-(\d+)/) : null;
        return match ? parseInt(match[1], 10) : 0;
      }).filter(n => !isNaN(n));
      maxPlotNumber = numbers.length ? Math.max(...numbers) : 0;
    }
    const now = new Date().toISOString();
    const newPlots: Plot[] = [];
    for (let i = 1; i <= count; i++) {
      newPlots.push({
        id: 0,
        plotNumber: `Plot-${maxPlotNumber + i}`,
        status: 'Available',
        area: 0,
        saleAmount: 0,
        customerName: '',
        customerPhone: '',
        customerEmail: '',
        hasSale: false,
        saleId: 0,
        amountPaid: 0,
        amountBalance: 0,
        selected: false,
        addedAt: now,
        propertyId: this.selectedPropertyId.value!
      });
    }
    this.showLoader(true);
      this.plotService.createPlotsBulk(newPlots, this.selectedPropertyId.value!).subscribe({
        next: (createdPlots) => {
          this.showLoader(false);
          this.showMessage('Plots added successfully.');
          this.refreshTable();
          this.refreshPlots.emit(this.selectedPropertyId.value!);
          this.showAddMultiple = false;
          this.multiplePlotCount = 1;
        },
        error: (err) => {
          this.showLoader(false);
          this.showMessage('Failed to add multiple plots.');
        }
      });
  }

  editPlot(plot: any, index: number) {
    this.editIndex = index;
    // Optionally clone plot data for cancel
    this.editingPlot = { ...plot };
  }

  savePlot(plot: any, index: number) {
    plot.propertyId = this.selectedPropertyId.value!;
    this.showLoader(true);
    this.plotService.updatePlot(plot, this.selectedPropertyId.value!).subscribe({
      next: (updatedPlot) => {
        this.showLoader(false);
        this.showMessage('Plot updated successfully.');
         this.refreshTable();
         this.refreshPlots.emit(this.selectedPropertyId.value!);
        this.editIndex = null;
      },
      error: (err) => {
        this.showLoader(false);
        this.showMessage('Failed to save plot changes.');
      }
    });
      // Removed duplicate emit
  }

  cancelEdit() {
    if (this.editIndex !== null && this.editingPlot) {
      // Optionally revert changes
      this.plots[this.editIndex] = { ...this.editingPlot };
    }
    this.editIndex = null;
  }

  // Add a close button to the popup
  closeModalClicked() {
    this.closeModal.emit();
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
}

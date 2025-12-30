import { Component, Input, Output, EventEmitter, OnChanges, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Property } from '../models/property.model';
import { Plot } from '../models/plot.model';
import { PropertyDetailsResponse } from '../models/property-details.model';
import { PropertyService } from '../services/property.service';

@Component({
  selector: 'app-property-profit-loss',
  templateUrl: './property-profit-loss.component.html',
  styleUrls: ['./property-profit-loss.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class PropertyProfitLossComponent implements OnChanges, OnInit {
  private propertyService = inject(PropertyService);
  
  @Input() property: Property | null = null;
  @Input() plots: Plot[] = [];
  @Output() closeModal = new EventEmitter<void>();

  // Property financial details
  buyValue = 0;
  amountPaidCash = 0;
  amountPaidLoan = 0;
  propertyBalance = 0;

  // Plots summary
  totalPlots = 0;
  totalPlotsSold = 0;
  totalPlotsSaleAmount = 0;
  totalPlotsPaid = 0;
  totalPlotsBalance = 0;
  totalBrokerage = 0;
  profitLossAfterLoan = 0;

  // Available / unrealized summary (computed from input plots when possible)
  totalAvailablePlots = 0;
  totalAvailablePotentialSale = 0;
  totalAvailablePotentialBalance = 0;

  // Realized / Unrealized metrics
  realizedReceived = 0; // amount actually received for sold plots
  realizedCosts = 0; // portion of costs allocated to sold plots
  realizedProfitLoss = 0;

  unrealizedReceivable = 0; // sold value not yet received

  // Loan details
  totalLoanPrinciple = 0;
  totalLoanInterest = 0;

  // Final profit/loss
  profitLoss = 0;

  // Allocation method: 'amount' = by sale amount, 'count' = by number of plots
  allocationMethod: 'amount' | 'count' = 'amount';

  ngOnInit() {
    this.loadPropertyDetails();
  }

  ngOnChanges() {
    if (this.property) {
      this.loadPropertyDetails();
    }
  }

  loadPropertyDetails() {
    if (!this.property?.id) return;
    
    this.propertyService.getPropertyDetails(this.property.id).subscribe({
      next: (response) => {
        // Property financial details
        this.buyValue = response.buyPrice || 0;
        this.amountPaidCash = response.amountPaid || 0;
        this.totalLoanPrinciple = response.totalLoanPrinciple || 0;
        this.propertyBalance = response.amountBalance || 0;

        // Plots summary
        this.totalPlots = response.totalPlots || 0;
        this.totalPlotsSaleAmount = response.totalSaleAmount || 0;
        this.totalPlotsPaid = response.totalPlotsPaid || 0;
        this.totalPlotsBalance = response.totalPlotsBalance || 0;
        this.totalBrokerage = response.totalBrokerage || 0;

        // Loan details
        this.totalLoanInterest = response.totalLoanInterest || 0;

        // Compute final net profit/loss per requested definition:
        // Net P/L = (cost of property + brokerage) - Total Sale Amount
        const totalCost = (this.buyValue || 0) + (this.totalBrokerage || 0);
        this.profitLoss = totalCost - (this.totalPlotsSaleAmount || 0);

        this.profitLossAfterLoan = response.profitLossAfterLoan;
        
        // Calculate plots sold using available and booked plots from API
        this.totalPlotsSold = response.bookedPlots || 0;
        // compute available/unrealized sums from input plots if provided
        this.computeAvailableFromInputPlots();
        this.computeRealizedUnrealized();
      },
      error: (error) => {
        console.error('Error loading property details:', error);
        // Fallback to local calculation if API fails
        this.calculateFromLocalData();
      }
    });
  }

  calculateFromLocalData() {
    if (!this.property) return;
    
    // Property details
    this.buyValue = this.property.buyPrice || 0;
    this.amountPaidCash = this.property.amountPaid || 0;
    this.totalLoanPrinciple = this.property.totalLoanPrinciple || 0;
    this.propertyBalance = this.property.amountBalance || 0;
    
    // Calculate plots summary
    const propertyPlots = this.plots.filter(p => p.propertyId === this.property?.id);
    this.totalPlots = propertyPlots.length;
    
    const soldPlots = propertyPlots.filter(p => p.hasSale);
    this.totalPlotsSold = soldPlots.length;
    this.totalPlotsSaleAmount = soldPlots.reduce((sum, p) => sum + (p.saleAmount || 0), 0);
    this.totalPlotsPaid = soldPlots.reduce((sum, p) => sum + (p.amountPaid || 0), 0);
    this.totalPlotsBalance = soldPlots.reduce((sum, p) => sum + (p.amountBalance || 0), 0);
    
    // TODO: Calculate brokerage from agent transactions
    this.totalBrokerage = 0;
    
    // TODO: Calculate loan interest from loan transactions
    this.totalLoanInterest = 0;
    
    // Calculate profit/loss
    // Net P/L = (Buy Price + Total Brokerage) - Total Sale Amount
    this.profitLoss = (this.buyValue || 0) + (this.totalBrokerage || 0) - (this.totalPlotsSaleAmount || 0);

    // Net P/L after loan and interest
    const totalCostAfterLoan = (this.buyValue || 0) + (this.totalLoanPrinciple || 0) + (this.totalLoanInterest || 0) + (this.totalBrokerage || 0);
    this.profitLossAfterLoan = totalCostAfterLoan - (this.totalPlotsSaleAmount || 0);

    // Compute available/unrealized sums from local plots
    this.computeAvailableFromInputPlots();
    this.computeRealizedUnrealized();
  }

  private computeRealizedUnrealized() {
    // realizedReceived = totalPlotsPaid
    this.realizedReceived = this.totalPlotsPaid || 0;

    // allocate property-level costs to realized portion.
    // Choose allocation method based on `allocationMethod`.
    // Total Costs = Buy Price + Total Loan Interest + Token Brokerage
    const totalCost = (this.buyValue || 0) + (this.totalLoanInterest || 0) + (this.totalBrokerage || 0);
    let allocationRatio = 0;
    if (this.allocationMethod === 'amount') {
      const totalSalePotential = (this.totalPlotsSaleAmount || 0) + (this.totalAvailablePotentialSale || 0);
      if (totalSalePotential > 0) {
        allocationRatio = (this.totalPlotsSaleAmount || 0) / totalSalePotential;
      } else if (this.totalPlots && this.totalPlots > 0) {
        allocationRatio = (this.totalPlotsSold / this.totalPlots);
      }
    } else {
      // count-based allocation
      allocationRatio = (this.totalPlots && this.totalPlots > 0) ? (this.totalPlotsSold / this.totalPlots) : 0;
    }
    this.realizedCosts = totalCost * allocationRatio;

    // realized profit/loss = amount received - allocated costs
    this.realizedProfitLoss = (this.realizedReceived || 0) - (this.realizedCosts || 0);

    // unrealized receivable = total sale amount (sold value) - amount received
    this.unrealizedReceivable = (this.totalPlotsSaleAmount || 0) - (this.totalPlotsPaid || 0);
  }

  setAllocationMethod(method: 'amount' | 'count') {
    this.allocationMethod = method;
    this.computeRealizedUnrealized();
  }

  getSegmentTitle(kind: 'received' | 'costs' | 'receivable') {
    const total = (this.realizedReceived || 0) + (this.realizedCosts || 0) + (this.unrealizedReceivable || 0);
    let value = 0;
    if (kind === 'received') value = this.realizedReceived || 0;
    if (kind === 'costs') value = this.realizedCosts || 0;
    if (kind === 'receivable') value = this.unrealizedReceivable || 0;
    const pct = total > 0 ? Math.round((value / total) * 100) : 0;
    return `${kind.charAt(0).toUpperCase() + kind.slice(1)}: ₹${value.toLocaleString()} (${pct}%)`;
  }

  private computeAvailableFromInputPlots() {
    if (!this.property) return;
    const propertyPlots = (this.plots || []).filter(p => p.propertyId === this.property?.id);
    const available = propertyPlots.filter(p => !p.hasSale);
    this.totalAvailablePlots = available.length;
    this.totalAvailablePotentialSale = available.reduce((sum, p) => sum + (p.saleAmount || 0), 0);
    this.totalAvailablePotentialBalance = available.reduce((sum, p) => sum + (p.amountBalance || 0), 0);
  }

  onClose() {
    this.closeModal.emit();
  }
}

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

  // Loan details
  totalLoanPrinciple = 0;
  totalLoanInterest = 0;

  // Final profit/loss
  profitLoss = 0;

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

        // Final profit/loss
        this.profitLoss = response.profitLossAfterLoan || 0;
        
        // Calculate plots sold using available and booked plots from API
        this.totalPlotsSold = response.bookedPlots || 0;
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
    // Profit/Loss = Total Sale Amount - (Buy Price + Total Loan Principle + Total Loan Interest + Total Brokerage)
    this.profitLoss = this.totalPlotsSaleAmount - (this.buyValue + this.totalLoanPrinciple + this.totalLoanInterest + this.totalBrokerage);
  }

  onClose() {
    this.closeModal.emit();
  }
}

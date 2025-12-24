namespace MyPropertyApi.Models
{
    public class PropertyDetailsSummaryDto
    {
        // Basic property information
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Type { get; set; } = "";
        public string Status { get; set; } = "";
        public string Owner { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public string Description { get; set; } = "";
        public string KhasraNo { get; set; } = "";
        public decimal Area { get; set; }

        // Financial details
        public decimal BuyPrice { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal TotalLoanPrinciple { get; set; }
        public decimal AmountBalance { get; set; }

        // Plot summary
        public int TotalPlots { get; set; }
        public int AvailablePlots { get; set; }
        public int BookedPlots { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public decimal TotalPlotsPaid { get; set; }
        public decimal TotalPlotsBalance { get; set; }
        public decimal TotalBrokerage { get; set; }

        // Loan summary
        public decimal TotalLoanInterest { get; set; }

        // Profit/Loss
        public decimal ProfitLossAfterLoan { get; set; }
    }
}

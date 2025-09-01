namespace RealEstateManager.Entities
{
    public class LoanTransaction
    {
        public int Id { get; set; }
        public int? PropertyId { get; set; }
        public int? PlotId { get; set; }
        public decimal LoanAmount { get; set; }
        public string LenderName { get; set; } = string.Empty;
        public decimal InterestRate { get; set; }
        public DateTime LoanDate { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Property { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalRepayable { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal Balance { get; set; }
        public int? Tenure { get; set; }
        public bool IsDeleted { get; set; }
    }
}
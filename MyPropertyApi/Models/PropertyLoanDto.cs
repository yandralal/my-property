public class PropertyLoanDto
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public string PropertyName { get; set; }
    public decimal LoanAmount { get; set; }
    public string LenderName { get; set; }
    public decimal InterestRate { get; set; }
    public int Tenure { get; set; }
    public decimal TotalInterest { get; set; }
    public decimal TotalRepayment { get; set; }
    public DateTime LoanDate { get; set; }
    public string Remarks { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
    public decimal TotalPaid { get; set; } 
    public decimal Outstanding { get; set; }

    // Added fields to show breakdown of amounts already paid
    public decimal TotalPrincipalPaid { get; set; }
    public decimal TotalInterestPaid { get; set; }
}
public class PropertyLoanTransactionDto
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public int PropertyLoanId { get; set; }
    public string LenderName { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public string TransactionType { get; set; }
    public DateTime TransactionDate { get; set; }
    public string PaymentMethod { get; set; }
    public string ReferenceNumber { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string ModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}
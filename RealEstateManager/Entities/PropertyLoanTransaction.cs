namespace RealEstateManager.Entities
{
    public class PropertyLoanTransaction
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int? PropertyLoanId { get; set; }
        public string LenderName { get; set; } = string.Empty;
        public decimal PrincipleAmount { get; set; }     
        public decimal InterestAmount { get; set; }      
        public string TransactionType { get; set; } = string.Empty; 
        public DateTime TransactionDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string? ReferenceNumber { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
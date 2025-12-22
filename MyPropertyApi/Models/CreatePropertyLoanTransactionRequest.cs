namespace MyPropertyApi.Models
{
    public class CreatePropertyLoanTransactionRequest
    {
        public int PropertyId { get; set; }
        public int PropertyLoanId { get; set; }
        public string LenderName { get; set; } = "";
        public string PayingFor { get; set; } = "";
        public string Amount { get; set; } = "";
        public string Interest { get; set; } = "";
        public string PaymentMethod { get; set; } = "";
        public string ReferenceNumber { get; set; } = "";
        public string Notes { get; set; } = "";
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } = "";
    }
}

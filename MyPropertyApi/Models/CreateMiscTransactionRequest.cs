using System;

namespace MyPropertyApi.Models
{
    public class CreateMiscTransactionRequest
    {
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string ReferenceNumber { get; set; }
        public string Recipient { get; set; }
        public string Notes { get; set; }
        public string TransactionType { get; set; }
    }
}

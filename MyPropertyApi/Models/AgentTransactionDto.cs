using System;

namespace MyPropertyApi.Models
{
    public class AgentTransactionDto
    {
        public int TransactionId { get; set; }
        public int AgentId { get; set; }
        public int PlotId { get; set; }
        public int PropertyId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string ReferenceNumber { get; set; }
        public string TransactionType { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string AgentName { get; set; }
        public string PropertyName { get; set; }
        public string PlotName { get; set; }
    }
}
public class CreateAgentTransactionRequest
{
    public int AgentId { get; set; }
    public int PlotId { get; set; }
    public int PropertyId { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
    public string ReferenceNumber { get; set; }
    public string TransactionType { get; set; }
    public string Notes { get; set; }
}
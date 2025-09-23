namespace MyPropertyApi.Models
{
    public class PlotDetailsDto
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int PropertyId { get; set; } 
        public string PlotNumber { get; set; } = "";
        public string Status { get; set; } = "";
        public decimal Area { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal SaleAmount { get; set; }
        public string CustomerName { get; set; } = "";
        public string CustomerPhone { get; set; } = "";
        public string CustomerEmail { get; set; } = "";
        public bool HasSale { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountBalance { get; set; }
        public int AgentId { get; set; }
        public decimal BrokerageAmount { get; set; }
        public string Description { get; set; } = "";
    }
}
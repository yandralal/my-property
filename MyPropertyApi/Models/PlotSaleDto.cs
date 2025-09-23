namespace MyPropertyApi.Models
{
    public class PlotSaleDto
    {
        public int SaleId { get; set; }
        public int PropertyId { get; set; }
        public int PlotId { get; set; }
        public string CustomerName { get; set; } = "";
        public string CustomerPhone { get; set; } = "";
        public string CustomerEmail { get; set; } = "";
        public decimal SaleAmount { get; set; }
        public DateTime? SaleDate { get; set; }
        public int AgentId { get; set; }
        public decimal BrokerageAmount { get; set; }
    }
}
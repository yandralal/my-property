namespace MyPropertyApi.Models
{
    public class PropertyDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Type { get; set; } = "";
        public string Status { get; set; } = "";
        public decimal BuyPrice { get; set; }
        public string Owner { get; set; } = "";
        public string Description { get; set; } = "";
        public string Phone { get; set; } = "";
        public decimal AmountPaid { get; set; }
        public decimal TotalLoanPrincipal { get; set; }
        public decimal AmountBalance { get; set; }
        public string KhasraNo { get; set; } = "";
        public decimal Area { get; set; }
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string ZipCode { get; set; } = "";
    }
}
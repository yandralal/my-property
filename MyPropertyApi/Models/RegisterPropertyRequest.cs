namespace MyPropertyApi.Models
{
    public class RegisterPropertyRequest
    {
        public int? Id { get; set; } 
        public string Title { get; set; } = "";
        public string Type { get; set; } = "";
        public string Status { get; set; } = "";
        public decimal Price { get; set; }
        public string Owner { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public string Description { get; set; } = "";
        public string KhasraNo { get; set; } = "";
        public decimal Area { get; set; }
    }
}
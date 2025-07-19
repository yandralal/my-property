namespace RealEstateManager.Entities
{
    public class Plot
    {
        public int? Id { get; set; }
        public string PlotNumber { get; set; } = "";
        public string Status { get; set; } = "Available";
        public decimal Area { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}

namespace MyPropertyApi.Models
{
    public class BulkDeletePlotsRequest
    {
        public List<int> PlotIds { get; set; } = new();
    }
}

using MyPropertyApi.Models;

public class BulkAddPlotsRequest
{
    public List<PlotDetailsDto> Plots { get; set; } = new();
}
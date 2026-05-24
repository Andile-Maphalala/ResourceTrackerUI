

namespace ResourceTrackerUI.Domain.Models.Feature.Inventory
{
    public class AggregatedInventoryItem
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; } = string.Empty;
        public string ComponentType { get; set; } = string.Empty;
        public string? ComponentImageUrl { get; set; }
        public int TotalQuantity { get; set; }
        public List<AggregatedLocation> Locations { get; set; } = new();
    }
}

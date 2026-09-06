

namespace ResourceTrackerUI.Domain.Models.Feature.Inventory.Consume
{
    public class ConsumeLinePreviewModel
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string? ImageUrl { get; set; }
        public int RequiredAmount { get; set; }
        public List<ConsumeLocationAllocationModel> Allocations { get; set; } = new();
        public bool Excluded { get; set; }

        public int AllocatedTotal => Allocations.Sum(a => a.AmountToDeduct);
        public int Shortfall => Math.Max(0, RequiredAmount - AllocatedTotal);
    }
}

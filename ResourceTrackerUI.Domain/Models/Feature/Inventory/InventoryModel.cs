

namespace ResourceTrackerUI.Domain.Models.Feature.Inventory
{
    public class InventoryModel : InventoryCrudModel
    {
        public string ComponentName { get; set; }
        public int ComponentType { get; set; }
        public string ComponentTypeName { get; set; }
        public string? ComponentImageUrl { get; set; }
        public int QuestId { get; set; }
        public string QuestName { get; set; }
        public string? QuestImageUrl { get; set; }
    }
}

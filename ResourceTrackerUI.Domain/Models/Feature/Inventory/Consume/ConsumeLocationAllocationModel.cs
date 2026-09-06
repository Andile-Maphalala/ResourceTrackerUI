

namespace ResourceTrackerUI.Domain.Models.Feature.Inventory.Consume
{
    public class ConsumeLocationAllocationModel
    {
        public int QuestComponentId { get; set; }
        public int QuestId { get; set; }
        public string QuestName { get; set; }
        public int AvailableQuantity { get; set; }
        public int AmountToDeduct { get; set; }
    }
}

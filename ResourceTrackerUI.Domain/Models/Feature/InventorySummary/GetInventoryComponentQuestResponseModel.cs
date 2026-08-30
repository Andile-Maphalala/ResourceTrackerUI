namespace ResourceTrackerUI.Domain.Models.Feature.InventorySummary
{
    public class GetInventoryComponentQuestResponseModel
    {
        public int QuestComponentId { get; set; }
        public int QuestId { get; set; }
        public string QuestName { get; set; }
        public int Quantity { get; set; }
    }
}

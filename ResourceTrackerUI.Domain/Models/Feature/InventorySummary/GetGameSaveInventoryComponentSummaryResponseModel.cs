namespace ResourceTrackerUI.Domain.Models.Feature.InventorySummary
{
    public class GetGameSaveInventoryComponentSummaryResponseModel
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentType { get; set; }
        public string ComponentImageUrl { get; set; }
        public int TotalQuantity { get; set; }
        public List<GetGameSaveInventoryComponentQuestModel> Quests { get; set; } = new List<GetGameSaveInventoryComponentQuestModel>();
    }
}

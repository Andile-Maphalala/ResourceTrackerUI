namespace ResourceTrackerUI.Domain.Models.Feature.InventorySummary
{
    public class GetGameSaveInventorySummaryResponseModel
    {
        public int Quests { get; set; }
        public int UniqueItems { get; set; }
        public int TotalItems { get; set; }
        public int ItemStacks { get; set; }
    }
}

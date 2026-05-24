

namespace ResourceTrackerUI.Domain.Models.Feature.Inventory
{
    public class InventoryBulkCrudModel 
    {
        public int QuestId { get; set; }
        public List<InventoryCrudModel> Commands { get; set; }
    }
}

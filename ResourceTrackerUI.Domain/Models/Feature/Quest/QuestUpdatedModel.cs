using ResourceTrackerUI.Domain.Enums;
using ResourceTrackerUI.Domain.Models.Feature.Inventory;

namespace ResourceTrackerUI.Domain.Models.Feature.Quest
{
    public class QuestUpdatedModel
    {
        public int QuestId { get; set; }
        public List<SearchInventoryResponseModel> Items { get; set; } = new List<SearchInventoryResponseModel>();
        public QuestUpdateTypeEnum UpdateType { get; set; }
    }
}

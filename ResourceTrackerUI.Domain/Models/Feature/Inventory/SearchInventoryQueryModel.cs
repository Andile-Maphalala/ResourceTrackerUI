

using ResourceTrackerUI.Domain.Models.Common;

namespace ResourceTrackerUI.Domain.Models.Feature.Inventory
{
    public class SearchInventoryQueryModel : PageableRequestModel
    {
        public int? Id { get; set; }
        public int? QuestId { get; set; }
        public string? QuestName { get; set; }
        public string? QuestDescription { get; set; }
        public int? ComponentId { get; set; }
        public string? ComponentName { get; set; }
        public string? ComponentDescription { get; set; }
        public int? Type { get; set; }
    }
}

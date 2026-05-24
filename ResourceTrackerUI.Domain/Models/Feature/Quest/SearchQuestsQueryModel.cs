using ResourceTrackerUI.Domain.Models.Common;

namespace ResourceTrackerUI.Domain.Models.Feature.Quest
{
    public class SearchQuestsQueryModel : PageableRequestModel
    {
        public int? QuestId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public int? GameId { get; set; }
        public string? GameName { get; set; }
        public int? GameSaveId { get; set; }
    }
}

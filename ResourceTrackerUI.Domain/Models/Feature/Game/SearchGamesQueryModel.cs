using ResourceTrackerUI.Domain.Models.Common;

namespace ResourceTrackerUI.Domain.Models.Feature.Game
{
    public class SearchGamesQueryModel : PageableRequestModel
    {
        public int? GameId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}

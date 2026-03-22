using ResourceTrackerUI.Domain.Models.Common;

namespace ResourceTrackerUI.Domain.Models.Feature
{
    public class SearchGamesQueryModel : PageableRequestModel
    {
        public int? GameId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? SearchTerms { get; set; }
    }
}

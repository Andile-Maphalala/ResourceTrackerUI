
using ResourceTrackerUI.Domain.Models.Common;

namespace ResourceTrackerUI.Domain.Models.Feature.Components
{
    public class SearchComponentQueryModel : PageableRequestModel
    {
        public int? ComponentId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Type { get; set; }
        public int? GameId { get; set; }
    }
}

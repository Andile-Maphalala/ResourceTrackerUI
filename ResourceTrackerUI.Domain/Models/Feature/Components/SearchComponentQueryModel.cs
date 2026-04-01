
using ResourceTrackerUI.Domain.Attributes;
using ResourceTrackerUI.Domain.Models.Common;

namespace ResourceTrackerUI.Domain.Models.Feature.Components
{
    public class SearchComponentQueryModel : PageableRequestModel
    {
        [FormField("Component Id")]
        public int? ComponentId { get; set; }

        [FormField("Name")]
        public string? Name { get; set; }

        [FormField("Description")]
        public string? Description { get; set; }

        [FormField("Component Type")]
        public int? Type { get; set; }

        [FormField("Game")]
        public int? GameId { get; set; }
    }
}

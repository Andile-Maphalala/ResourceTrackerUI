using ResourceTrackerUI.Domain.Attributes;
using ResourceTrackerUI.Domain.Models.Common;

namespace ResourceTrackerUI.Domain.Models.Feature.Game
{
    public class SearchGamesQueryModel : PageableRequestModel
    {
        [FormFieldAttribute("Game Id")]
        public int? GameId { get; set; }

        [FormFieldAttribute("Name")]
        public string? Name { get; set; }

        [FormFieldAttribute("Description")]
        public string? Description { get; set; }
    }
}

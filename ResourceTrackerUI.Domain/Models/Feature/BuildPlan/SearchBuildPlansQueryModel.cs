using ResourceTrackerUI.Domain.Models.Common;

namespace ResourceTrackerUI.Domain.Models.Feature.BuildPlan
{
    public class SearchBuildPlansQueryModel : PageableRequestModel
    {
        public int? BuildPlanId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? GameId { get; set; }
        public string? GameName { get; set; }
        public int? GameSaveId { get; set; }
    }
}

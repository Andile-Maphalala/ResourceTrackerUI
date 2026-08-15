

using ResourceTrackerUI.Domain.Models.Common;

namespace ResourceTrackerUI.Domain.Models.Feature.BuildPlanComponent
{
    public class SearchBuildPlanComponentQueryModel : PageableRequestModel
    {
        public int? Id { get; set; }
        public int? BuildPlanId { get; set; }
        public string? BuildPlanName { get; set; }
        public string? BuildPlanDescription { get; set; }
        public int? ComponentId { get; set; }
        public string? ComponentName { get; set; }
        public string? ComponentDescription { get; set; }
        public int? Type { get; set; }
    }
}

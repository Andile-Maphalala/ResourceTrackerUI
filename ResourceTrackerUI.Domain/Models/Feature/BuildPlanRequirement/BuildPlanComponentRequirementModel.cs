

namespace ResourceTrackerUI.Domain.Models.Feature.BuildPlanRequirement
{
    public class BuildPlanComponentRequirementModel
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public int RequiredAmount { get; set; }
        public int AvailableAmount { get; set; }
        public int MissingAmount { get; set; }
        public int Type { get; set; }
        public string? ImageUrl { get; set; }
    }
}

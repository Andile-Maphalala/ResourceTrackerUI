

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
        public List<ComponentLocationModel> Locations { get; set; } = new();

        public string GetComponentTypeName()
        {
            return Type switch
            {
                1 => "Resource",
                2 => "Composite",
                3 => "Facility",
                _ => "Unknown"
            };
        }
    }
}

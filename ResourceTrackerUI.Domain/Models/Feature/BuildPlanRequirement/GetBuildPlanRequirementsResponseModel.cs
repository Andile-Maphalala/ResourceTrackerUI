

namespace ResourceTrackerUI.Domain.Models.Feature.BuildPlanRequirement
{
    public class GetBuildPlanRequirementsResponseModel
    {
        public int BuildPlanId { get; set; }
        public string BuildPlanName { get; set; }
        public int TotalRequired { get; set; }
        public int TotalAvailable { get; set; }
        public int TotalMissing { get; set; }
        public List<BuildPlanComponentRequirementModel> Requirements { get; set; }
        public List<BuildPlanFacilityRequirementModel> FacilityRequirements { get; set; }
    }
}

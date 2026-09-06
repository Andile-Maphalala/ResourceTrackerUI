

namespace ResourceTrackerUI.Domain.Models.Feature.BuildPlanRequirement
{
    public class GetBuildPlanSankeyResponseModel
    {
        public int BuildPlanId { get; set; }
        public string BuildPlanName { get; set; }
        public List<SankeyEdgeModel> Edges { get; set; }
        public List<ComponentFacilityModel> CraftingStations { get; set; }
    }
}

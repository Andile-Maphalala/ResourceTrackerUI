

namespace ResourceTrackerUI.Domain.Models.Feature.BuildPlanComponent
{
    public class BuildPlanComponentModel
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int QuantityNeeded { get; set; }
        public int ComponentId { get; set; }
        public int BuildPlanId { get; set; }
    }
}

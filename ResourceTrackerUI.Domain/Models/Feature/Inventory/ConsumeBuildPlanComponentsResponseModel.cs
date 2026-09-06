

namespace ResourceTrackerUI.Domain.Models.Feature.Inventory
{
    public class ConsumeBuildPlanComponentsResponseModel
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}

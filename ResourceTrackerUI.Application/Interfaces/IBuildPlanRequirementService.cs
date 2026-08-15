

using ResourceTrackerUI.Domain.Models.Feature.BuildPlanRequirement;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IBuildPlanRequirementService
    {
        Task<GetBuildPlanRequirementsResponseModel> GetBuildPlanRequirements(int buildPlanId, bool includeFacilityRequirements, bool IncludeInventory, CancellationToken cancellationToken);
    }
}

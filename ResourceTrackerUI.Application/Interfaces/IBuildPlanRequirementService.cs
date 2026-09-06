

using ResourceTrackerUI.Domain.Models.Feature.BuildPlanRequirement;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IBuildPlanRequirementService
    {
        Task<GetBuildPlanRequirementsResponseModel> GetBuildPlanRequirements(int buildPlanId, CancellationToken cancellationToken);
        Task<GetBuildPlanSankeyResponseModel> GetBuildPlanSankey(int buildPlanId, CancellationToken cancellationToken);
    }
}

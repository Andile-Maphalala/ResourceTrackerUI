

using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Domain.Models.Feature.BuildPlanRequirement;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IBuildPlanRequirementService
    {
        Task<GetBuildPlanRequirementsResponseModel> GetBuildPlanRequirements(int buildPlanId, CancellationToken cancellationToken);
        Task<GetBuildPlanSankeyResponse> GetBuildPlanSankey(int buildPlanId, CancellationToken cancellationToken);
    }
}

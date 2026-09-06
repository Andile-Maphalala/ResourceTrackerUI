

using MapsterMapper;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Feature.BuildPlanRequirement;

namespace ResourceTrackerUI.Application.Services.Feature
{
    public class BuildPlanRequirementService(ResourceTrackerApiClient apiClient, IMapper mapper) : IBuildPlanRequirementService
    {
        public async Task<GetBuildPlanRequirementsResponseModel> GetBuildPlanRequirements(int buildPlanId, CancellationToken cancellationToken)
        {
            var response = await apiClient.ApiBuildPlanRequirementGetBuildPlanRequirementAsync(buildPlanId, cancellationToken);
            return mapper.Map<GetBuildPlanRequirementsResponseModel>(response);
        }

        public async Task<GetBuildPlanSankeyResponseModel> GetBuildPlanSankey(int buildPlanId, CancellationToken cancellationToken)
        {
            var response = await apiClient.ApiBuildPlanRequirementGetBuildPlanSankeyAsync(buildPlanId, cancellationToken);
            return mapper.Map<GetBuildPlanSankeyResponseModel>(response);
        }
    }
}



using MapsterMapper;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Feature.BuildPlanRequirement;

namespace ResourceTrackerUI.Application.Services.Feature
{
    public class BuildPlanRequirementService(ResourceTrackerApiClient apiClient, IMapper mapper) : IBuildPlanRequirementService
    {
        public async Task<GetBuildPlanRequirementsResponseModel> GetBuildPlanRequirements(int buildPlanId, bool includeFacilityRequirements, bool IncludeInventory, CancellationToken cancellationToken)
        {
            var response = await apiClient.ApiBuildPlanRequirementGetBuildPlanRequirementAsync(buildPlanId, includeFacilityRequirements, IncludeInventory, cancellationToken);
            return mapper.Map<GetBuildPlanRequirementsResponseModel>(response);
        }
    }
}



using MapsterMapper;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.BuildPlan;

namespace ResourceTrackerUI.Application.Services.Feature
{
    public class BuildPlanService(ResourceTrackerApiClient apiClient, IMapper mapper) : IBuildPlanService
    {
        public async Task<BuildPlanModel> CreateBuildPlan(BuildPlanModel model, CancellationToken cancellationToken)
        {
            var dto = mapper.Map<CreateBuildPlanCommand>(model);
            var response = await apiClient.ApiBuildPlanCreateBuildPlanAsync(dto, cancellationToken);
            var result = mapper.Map<BuildPlanModel>(response);
            return result;
        }

        public async Task UpdateBuildPlan(BuildPlanModel model, CancellationToken cancellationToken)
        {
            var dto = mapper.Map<UpdateBuildPlanCommand>(model);
            await apiClient.ApiBuildPlanUpdateBuildPlanAsync(dto, cancellationToken);
        }

        public async Task DeleteBuildPlan(int id, CancellationToken cancellationToken)
        {
            await apiClient.ApiBuildPlanDeleteBuildPlanAsync(id, cancellationToken);
        }

        public async Task<BuildPlanModel> GetBuildPlan(int id, CancellationToken cancellationToken)
        {
            var response = await apiClient.ApiBuildPlanGetBuildPlanAsync(id, cancellationToken);
            var result = mapper.Map<BuildPlanModel>(response);
            return result;
        }

        public async Task<PageableResponseModel<SearchBuildPlansResponseModel>> SearchBuildPlan(SearchBuildPlansQueryModel model, CancellationToken cancellationToken)
        {
            var orderDirection = (OrderDirectionEnum?)model.OrderDirection;
            if (string.IsNullOrEmpty(model.OrderBy))
                model.OrderBy = "Id";
            var response = await apiClient.ApiBuildPlanSearchBuildPlanAsync(model.BuildPlanId, model.Name, model.Description, model.GameId, model.GameName,model.GameSaveId, model.SearchTerms, model.PageNumber, model.PageSize, model.OrderBy, orderDirection, cancellationToken);
            var result = mapper.Map<PageableResponseModel<SearchBuildPlansResponseModel>>(response);
            return result;
        }
 
    }
}

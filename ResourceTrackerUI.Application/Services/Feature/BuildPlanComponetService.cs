

using MapsterMapper;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.BuildPlanComponent;

namespace ResourceTrackerUI.Application.Services.Feature
{
    public class BuildPlanComponetService(ResourceTrackerApiClient apiClient, IMapper mapper) : IBuildPlanComponetService
    {
        public async Task<int> CreateBuildPlanBulkCompoent(int buildPlanId, List<BuildPlanComponentModel> models, CancellationToken cancellationToken)
        {
            var componentDto = mapper.Map<List<CreateBuildPlanComponentDto>>(models);
            CreateBuildPlanComponentsCommand dto = new CreateBuildPlanComponentsCommand
            {
                BuildPlanId = buildPlanId,
                Commands = componentDto
            };
            var result = await apiClient.ApiBuildPlanComponetCreateBuildPlanCompoentBulkAsync(dto, cancellationToken);
            return result.Count;
        }

        public async Task<int> CreateBuildPlanCompoent(BuildPlanComponentModel model, CancellationToken cancellationToken)
        {
           var dto = mapper.Map<CreateBuildPlanComponentCommand>(model);
           var result = await apiClient.ApiBuildPlanComponetCreateBuildPlanCompoentAsync(dto, cancellationToken);
           return result.Id;
        }

        public async Task UpdateBuildPlanBulkCompoent(List<BuildPlanComponentModel> models, CancellationToken cancellationToken)
        {
            var listDto = mapper.Map<List<UpdateBuildPlanComponentCommand>>(models);
            var dto = new UpdateBuildPlanComponentsCommand
            {
                Commands = listDto
            };
            await apiClient.ApiBuildPlanComponetUpdateBuildPlanComponentBulkAsync(dto, cancellationToken);
        }

        public async Task UpdateBuildPlanCompoent(BuildPlanComponentModel model, CancellationToken cancellationToken)
        {
            var dto = mapper.Map<UpdateBuildPlanComponentCommand>(model);
            await apiClient.ApiBuildPlanComponetUpdateBuildPlanComponentAsync(dto, cancellationToken);
        }

        public async Task<BuildPlanComponentResponseModel> GetBuildPlanComponent(int id, CancellationToken cancellationToken)
        {
            var response = await apiClient.ApiBuildPlanComponetGetBuildPlanComponentByIdAsync(id, cancellationToken);
            var result = mapper.Map<BuildPlanComponentResponseModel>(response);
            return result;

        }

        public async Task<PageableResponseModel<BuildPlanComponentResponseModel>> SearchBuildPlanComponent(SearchBuildPlanComponentQueryModel model, CancellationToken cancellationToken)
        {
            var orderDirection = (OrderDirectionEnum?)model.OrderDirection;
            var response = await apiClient.ApiBuildPlanComponetSearchBuildPlanComponentsAsync(model.Id,model.BuildPlanId,model.BuildPlanName,model.BuildPlanDescription,model.ComponentId,model.ComponentName,model.ComponentDescription,model.Type,model.SearchTerms, model.PageNumber,model.PageSize,model.OrderBy,orderDirection,cancellationToken);
            var result = mapper.Map<PageableResponseModel<BuildPlanComponentResponseModel>>(response);
            return result;
        }

       
    }
}

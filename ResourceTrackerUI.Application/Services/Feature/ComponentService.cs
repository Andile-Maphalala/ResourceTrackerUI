

using MapsterMapper;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.Components;

namespace ResourceTrackerUI.Application.Services.Feature
{
    

    public class ComponentService : IComponentService
    {
        private ResourceTrackerApiClient _apiClient { get; set; }
        private IMapper _mapper { get; set; }
        public ComponentService(ResourceTrackerApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }
        public async Task<int> CreateComponent(ComponentModel model, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateComponentCommand>(model);
            var result = await _apiClient.ApiComponentCreateCompoentAsync(dto, cancellationToken);
            return result.Id;
        }

        public async Task UpdateComponent(ComponentModel model, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UpdateComponentCommand>(model);
            await _apiClient.ApiComponentUpdateComponentAsync(dto, cancellationToken);
        }

        public async Task DeleteComponent(int id, CancellationToken cancellationToken)
        {
            await _apiClient.ApiComponentDeleteComponentAsync(id, cancellationToken);
        }

        public async Task<ComponentModel> GetComponent(int Id, CancellationToken cancellationToken)
        {
            var response = await _apiClient.ApiComponentGetComponentAsync(Id, cancellationToken);
            var result = _mapper.Map<ComponentModel>(response);
            return result;
        }

        public async Task<PageableResponseModel<SearchComponentRequestModel>> SearchComponent(SearchComponentQueryModel model, CancellationToken cancellationToken)
        {
            var orderDirection = (OrderDirectionEnum?)model.OrderDirection;
            var response = await _apiClient.ApiComponentSearchComponentAsync(model.ComponentId, model.Name, model.Description, model.Type, model.GameId, model.SearchTerms, model.PageNumber, model.PageSize,model.OrderBy, orderDirection, cancellationToken);
            var result = _mapper.Map<PageableResponseModel<SearchComponentRequestModel>>(response);
            return result;
        }
        
    }
}

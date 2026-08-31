

using MapsterMapper;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.Inventory;
using ResourceTrackerUI.Domain.Models.Feature.InventorySummary;

namespace ResourceTrackerUI.Application.Services.Feature
{
    public class InventoryService : IInventoryService
    {

        private ResourceTrackerApiClient _apiClient { get; set; }
        private IMapper _mapper { get; set; }

        public InventoryService(ResourceTrackerApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        public async Task<int> CreateInventory(InventoryModel model, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateQuestComponentCommand>(model);
            var result = await _apiClient.ApiQuestComponentCreateQuestCompoentAsync(dto, cancellationToken);
            return result.Id;
        }

        public async Task<int> CreateInventoryBulk(InventoryBulkCrudModel model, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateQuestComponentsCommand>(model);
            var result = await _apiClient.ApiQuestComponentCreateQuestCompoentBulkAsync(dto, cancellationToken);
            return result.Count;
        }

        public async Task UpdateInventory(InventoryCrudModel model, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UpdateQuestComponentCommand>(model);
            await _apiClient.ApiQuestComponentUpdateQuestComponentAsync(dto, cancellationToken);
        }

        public async Task UpdateInventoryBulk(InventoryBulkCrudModel model, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UpdateQuestComponentsCommand>(model);
            await _apiClient.ApiQuestComponentUpdateQuestComponentBulkAsync(dto, cancellationToken);
        }

        public async Task DeleteInventory(int Id, CancellationToken cancellationToken)
        {
            await _apiClient.ApiQuestComponentDeleteQuestComponentAsync(Id, cancellationToken);
        }

        public async Task DeleteInventoryBulk(List<int> Ids, CancellationToken cancellationToken)
        {
            var dto = new DeleteQuestComponentsCommand { Ids = Ids };
            await _apiClient.ApiQuestComponentDeleteQuestComponentBulkAsync(dto, cancellationToken);
        }

        public async Task<InventoryModel> GetInventory(int Id, CancellationToken cancellationToken)
        {
            var result = await _apiClient.ApiQuestComponentGetQuestComponentAsync(Id, cancellationToken);
            return _mapper.Map<InventoryModel>(result);
        }

        public async Task<PageableResponseModel<SearchInventoryResponseModel>> SearchInventory(SearchInventoryQueryModel query, CancellationToken cancellationToken)
        {
            var orderDirection = (OrderDirectionEnum?)query.OrderDirection;
            var result = await _apiClient.ApiQuestComponentSearchQuestComponentAsync(query.Id, query.QuestId, query.QuestName, query.QuestDescription, query.ComponentId, query.ComponentName, query.ComponentDescription, query.Type, query.SearchTerms, query.PageNumber, query.PageSize,query.OrderBy, orderDirection, cancellationToken);
            return _mapper.Map<PageableResponseModel<SearchInventoryResponseModel>>(result);

        }

        public async Task<List<GetInventoryComponentQuestResponseModel>> GetInventoryComponentQuests(int componentId, int buildPlanId, CancellationToken cancellationToken)
        {
            var response = await _apiClient.ApiInventoryGetInventoryComponentQuestAsync(componentId, buildPlanId, cancellationToken);
            var result = _mapper.Map<List<GetInventoryComponentQuestResponseModel>>(response);
            return result;
        }
    }
}

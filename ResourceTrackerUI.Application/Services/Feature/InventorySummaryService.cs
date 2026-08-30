

using MapsterMapper;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Feature.InventorySummary;

namespace ResourceTrackerUI.Application.Services.Feature
{
    public class InventorySummaryService(ResourceTrackerApiClient apiClient, IMapper mapper) : IInventorySummaryService
    {
        public async Task<List<GetGameSaveInventoryComponentSummaryResponseModel>> GetGameSaveInventoryComponentSummary(int gameSaveId, CancellationToken cancellationToken)
        {
            var response = await apiClient.ApiInventoryGetGameSaveInventoryComponentSummaryAsync(gameSaveId, cancellationToken);
            var result = mapper.Map<List<GetGameSaveInventoryComponentSummaryResponseModel>>(response);
            return result;
        }

        public async Task<GetGameSaveInventorySummaryResponseModel> GetGameSaveInventorySummary(int gameSaveId, CancellationToken cancellationToken)
        {
            var response = await apiClient.ApiInventoryGetGameSaveInventorySummaryAsync(gameSaveId, cancellationToken);
            var result = mapper.Map<GetGameSaveInventorySummaryResponseModel>(response);
            return result;
        }

        public async Task<List<GetInventoryComponentQuestResponseModel>> GetInventoryComponentQuest(int componentId, int buildPlanId, CancellationToken cancellationToken)
        {
            var response = await apiClient.ApiInventoryGetInventoryComponentQuestAsync(componentId, buildPlanId, cancellationToken);
            var result = mapper.Map<List<GetInventoryComponentQuestResponseModel>>(response);
            return result;
        }
    }
}

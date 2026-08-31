
using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.Inventory;
using ResourceTrackerUI.Domain.Models.Feature.InventorySummary;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<int> CreateInventory(InventoryModel model, CancellationToken cancellationToken);
        Task<int> CreateInventoryBulk(InventoryBulkCrudModel model, CancellationToken cancellationToken);
        Task UpdateInventory(InventoryCrudModel model, CancellationToken cancellationToken);
        Task UpdateInventoryBulk(InventoryBulkCrudModel model, CancellationToken cancellationToken);
        Task DeleteInventory(int Id, CancellationToken cancellationToken);
        Task DeleteInventoryBulk(List<int> Ids, CancellationToken cancellationToken);
        Task<InventoryModel> GetInventory(int Id, CancellationToken cancellationToken);
        Task<PageableResponseModel<SearchInventoryResponseModel>> SearchInventory(SearchInventoryQueryModel query, CancellationToken cancellationToken);
        Task<List<GetInventoryComponentQuestResponseModel>> GetInventoryComponentQuests(int componentId, int buildPlanId, CancellationToken cancellationToken);

    }
}

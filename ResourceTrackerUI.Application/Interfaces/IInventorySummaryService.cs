

using ResourceTrackerUI.Domain.Models.Feature.InventorySummary;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IInventorySummaryService
    {
        Task<List<GetGameSaveInventoryComponentSummaryResponseModel>> GetGameSaveInventoryComponentSummary(int gameSaveId, CancellationToken cancellationToken);
        Task<GetGameSaveInventorySummaryResponseModel> GetGameSaveInventorySummary(int gameSaveId, CancellationToken cancellationToken);
    }
}

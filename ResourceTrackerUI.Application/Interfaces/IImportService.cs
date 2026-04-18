

using Microsoft.AspNetCore.Components.Forms;
using ResourceTrackerUI.Domain.Models.Feature.Import;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IImportService
    {
        Task<ImportGameResponseModel> ImportGameComponets(int? gameId, IBrowserFile importJsonFile, CancellationToken cancellationToken);
    }
}



using Microsoft.AspNetCore.Components.Forms;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Feature.Import;

namespace ResourceTrackerUI.Application.Services.Feature
{
    public class ImportService: IImportService
    {
        private ResourceTrackerApiClient _apiClient { get; set; }

        public ImportService(ResourceTrackerApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ImportGameResponseModel> ImportGameComponets(int? gameId, IBrowserFile importJsonFile, CancellationToken cancellationToken)
        {
           var fileparam = new FileParameter(importJsonFile.OpenReadStream(5242880), importJsonFile.Name, importJsonFile.ContentType);
           var response = await _apiClient.ApiImportImportGameComponentsAsync(gameId, fileparam, cancellationToken);
           var result = new ImportGameResponseModel
           {
               TotalFacilities = response.TotalFacilities,
               TotalComponet = response.TotalComponet
           };

            return result;
        }
    }
}

using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.BuildPlanComponent;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IBuildPlanComponentService
    {
        Task<int> CreateBuildPlanComponent(BuildPlanComponentModel model, CancellationToken cancellationToken);
        Task<int> CreateBuildPlanBulkComponent(int buildPlanId, List<BuildPlanComponentModel> models, CancellationToken cancellationToken);
        Task UpdateBuildPlanComponent(BuildPlanComponentModel model, CancellationToken cancellationToken);
        Task UpdateBuildPlanBulkComponent(List<BuildPlanComponentModel> models, CancellationToken cancellationToken);
        Task DeleteBuildPlanComponent(int id, CancellationToken cancellationToken);
        Task DeleteBuildPlanBulkComponent(List<int> ids, CancellationToken cancellationToken);
        Task<BuildPlanComponentResponseModel> GetBuildPlanComponent(int id, CancellationToken cancellationToken);
        Task<PageableResponseModel<BuildPlanComponentResponseModel>> SearchBuildPlanComponents(SearchBuildPlanComponentQueryModel model, CancellationToken cancellationToken);
    }
}

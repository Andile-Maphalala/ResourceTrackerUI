

using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.BuildPlanComponent;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IBuildPlanComponetService
    {
        Task<int> CreateBuildPlanCompoent(BuildPlanComponentModel model, CancellationToken cancellationToken);
        Task<int> CreateBuildPlanBulkCompoent(int buildPlanId, List<BuildPlanComponentModel> models, CancellationToken cancellationToken);
        Task UpdateBuildPlanCompoent(BuildPlanComponentModel model, CancellationToken cancellationToken);
        Task UpdateBuildPlanBulkCompoent(List<BuildPlanComponentModel> models, CancellationToken cancellationToken);
        Task<BuildPlanComponentResponseModel> GetBuildPlanComponent(int id, CancellationToken cancellationToken);
        Task<PageableResponseModel<BuildPlanComponentResponseModel>> SearchBuildPlanComponent(SearchBuildPlanComponentQueryModel model, CancellationToken cancellationToken);
    }
}

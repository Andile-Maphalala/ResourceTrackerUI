

using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.BuildPlan;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IBuildPlanService
    {
        Task<BuildPlanModel> CreateBuildPlan(BuildPlanModel model, CancellationToken cancellationToken);
        Task UpdateBuildPlan(BuildPlanModel model, CancellationToken cancellationToken);
        Task DeleteBuildPlan(int id, CancellationToken cancellationToken);
        Task<BuildPlanModel> GetBuildPlan(int id, CancellationToken cancellationToken);
        Task<PageableResponseModel<SearchBuildPlansResponseModel>> SearchBuildPlan(SearchBuildPlansQueryModel model, CancellationToken cancellationToken);
    }
}

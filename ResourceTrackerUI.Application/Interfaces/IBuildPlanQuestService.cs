

using ResourceTrackerUI.Domain.Models.Feature.BuildPlanQuest;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IBuildPlanQuestService
    {
        Task<int> CreateBuildPlanQuest(int buildPlanId, int questId, CancellationToken cancellationToken);
        Task DeleteBuildPlanQuest(int buildPlanId, int questId, CancellationToken cancellationToken);
        Task<List<GetBuildPlanQuestListResponseModel>> GetBuildPlanQuestList(int? buildPlanId, int? questId, CancellationToken cancellationToken);
    }
}

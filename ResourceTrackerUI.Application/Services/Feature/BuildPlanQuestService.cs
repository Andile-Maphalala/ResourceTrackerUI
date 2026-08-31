

using MapsterMapper;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Feature.BuildPlanQuest;

namespace ResourceTrackerUI.Application.Services.Feature
{
    public class BuildPlanQuestService(ResourceTrackerApiClient apiClient, IMapper mapper) : IBuildPlanQuestService
    {
        public async Task<int> CreateBuildPlanQuest(int buildPlanId, int questId, CancellationToken cancellationToken)
        {
            var dto = new CreateBuildPlanQuestCommand
            {
                BuildPlanId = buildPlanId,
                QuestId = questId
            };
            var response = await apiClient.ApiBuildPlanQuestCreateBuildPlanQuestAsync(dto, cancellationToken);
            return response.Id;
        }

        public async Task DeleteBuildPlanQuest(int buildPlanId, int questId, CancellationToken cancellationToken)
        {
            await apiClient.ApiBuildPlanQuestDeleteBuildPlanQuestAsync(buildPlanId, questId, cancellationToken);
        }

        public async Task<List<GetBuildPlanQuestListResponseModel>> GetBuildPlanQuestList(int? buildPlanId, int? questId, CancellationToken cancellationToken)
        {
            var response = await apiClient.ApiBuildPlanQuestGetBuildPlanQuestListAsync(buildPlanId, questId, cancellationToken);
            var result = mapper.Map<List<GetBuildPlanQuestListResponseModel>>(response);
            return result;
        }
    }
}

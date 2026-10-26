using MapsterMapper;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Feature.PlayerFacility;

namespace ResourceTrackerUI.Application.Services.Feature
{
    public class PlayerFacilityService(ResourceTrackerApiClient apiClient, IMapper mapper) : IPlayerFacilityService
    {
        public async Task<List<PlayerFacilityModel>> GetPlayerFacilitiesByQuest(int questId, CancellationToken cancellationToken)
        {
            var response = await apiClient.ApiPlayerFacilityGetPlayerFacilitiesByQuestAsync(questId, cancellationToken);
            return response.Select(mapper.Map<PlayerFacilityModel>).ToList();
        }

        public async Task<PlayerFacilityModel> GetPlayerFacility(int id, CancellationToken cancellationToken)
        {
            var response = await apiClient.ApiPlayerFacilityGetPlayerFacilityAsync(id, cancellationToken);
            return mapper.Map<PlayerFacilityModel>(response);
        }

        public async Task<int> CreatePlayerFacility(int componentId, int questId, int level, CancellationToken cancellationToken)
        {
            var response = await apiClient.ApiPlayerFacilityCreatePlayerFacilityAsync(
                new CreatePlayerFacilityCommand
                {
                    ComponentId = componentId,
                    QuestId = questId,
                    Level = level
                },
                cancellationToken);

            return response.Id;
        }

        public async Task UpdatePlayerFacility(int id, int level, CancellationToken cancellationToken)
        {
            await apiClient.ApiPlayerFacilityUpdatePlayerFacilityAsync(
                new UpdatePlayerFacilityCommand
                {
                    Id = id,
                    Level = level
                },
                cancellationToken);
        }

        public async Task DeletePlayerFacility(int id, CancellationToken cancellationToken)
        {
            await apiClient.ApiPlayerFacilityDeletePlayerFacilityAsync(id, cancellationToken);
        }
    }
}

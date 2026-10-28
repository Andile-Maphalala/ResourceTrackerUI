using ResourceTrackerUI.Domain.Models.Feature.PlayerFacility;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IPlayerFacilityService
    {
        Task<List<PlayerFacilityModel>> GetPlayerFacilitiesByQuest(int questId, CancellationToken cancellationToken);
        Task<PlayerFacilityModel> GetPlayerFacility(int id, CancellationToken cancellationToken);
        Task<int> CreatePlayerFacility(int componentId, int questId, int level, CancellationToken cancellationToken);
        Task UpdatePlayerFacility(int id, int level, CancellationToken cancellationToken);
        Task DeletePlayerFacility(int id, CancellationToken cancellationToken);
    }
}

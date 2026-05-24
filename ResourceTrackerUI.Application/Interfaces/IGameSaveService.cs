

using ResourceTrackerUI.Domain.Models.Feature.GameSave;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IGameSaveService
    {
        Task<int> CreateGameSave(GameSaveModel model, CancellationToken cancellationToken);
        Task UpdateGameSave(GameSaveModel model, CancellationToken cancellationToken);
        Task DeleteGameSave(int id, CancellationToken cancellationToken);
        Task<GameSaveModel> GetGameSave(int id, CancellationToken cancellationToken);
        Task<List<GameSaveListModel>> GetGameSavesForGame(int gameId, CancellationToken cancellationToken);
    }
}

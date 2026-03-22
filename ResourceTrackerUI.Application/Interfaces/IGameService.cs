
using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature;
using ResourceTrackerUI.Domain.Models.Feature.Query;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IGameService
    {
        Task<int> CreateGame(GameModel model, CancellationToken cancellationToken);
        Task UpdateGame(GameModel model, CancellationToken cancellationToken);
        Task DeleteGame(int id, CancellationToken cancellationToken);
        Task<GameModel> GetGame(int Id, CancellationToken cancellationToken);
        Task<PageableResponseModel<SearchGamesResponseModel>> SearchGame(SearchGamesQueryModel model, CancellationToken cancellationToken);
    }
}


using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.Command;
using ResourceTrackerUI.Domain.Models.Feature.Query;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IGameService
    {
        Task<int> CreateGame(CreateGameCommandModel model, CancellationToken cancellationToken);
        Task UpdateGame(UpdateGameCommandModel model, CancellationToken cancellationToken);
        Task DeleteGame(int id, CancellationToken cancellationToken);
        Task<GetGameResponseModel> GetGame(int Id, CancellationToken cancellationToken);
        Task<PageableResponseModel<SearchGamesResponseModel>> SearchGame(SearchGamesQueryModel model, CancellationToken cancellationToken);
    }
}

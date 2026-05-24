

using MapsterMapper;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Feature.GameSave;

namespace ResourceTrackerUI.Application.Services.Feature
{
    public class GameSaveService : IGameSaveService
    {
        private ResourceTrackerApiClient _apiClient { get; set; }
        private IMapper _mapper { get; set; }

        public GameSaveService(ResourceTrackerApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        public async Task<int> CreateGameSave(GameSaveModel model, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateGameSaveCommand>(model);
            var result = await _apiClient.ApiGameSaveCreateGameSaveAsync(dto, cancellationToken);
            return result.Id;
        }

        public async Task UpdateGameSave(GameSaveModel model, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UpdateGameSaveCommand>(model);
            await _apiClient.ApiGameSaveUpdateGameSaveAsync(dto, cancellationToken);
        }

        public async Task DeleteGameSave(int id, CancellationToken cancellationToken)
        {
            await _apiClient.ApiGameSaveDeleteGameSaveAsync(id, cancellationToken);
        }

        public async Task<GameSaveModel> GetGameSave(int id, CancellationToken cancellationToken)
        {
            var result = await _apiClient.ApiGameSaveGetGameSaveAsync(id, cancellationToken);
            return _mapper.Map<GameSaveModel>(result);
        }

        public async Task<List<GameSaveListModel>> GetGameSavesForGame(int gameId, CancellationToken cancellationToken)
        {
            var result = await _apiClient.ApiGameSaveGetGameSaveListAsync(gameId, cancellationToken);
            return _mapper.Map<List<GameSaveListModel>>(result);
        }
    }
}

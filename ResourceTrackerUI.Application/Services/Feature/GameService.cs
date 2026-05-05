
using MapsterMapper;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.Game;

namespace ResourceTrackerUI.Application.Services.Feature
{
    public class GameService : IGameService
    {
        private ResourceTrackerApiClient _apiClient { get; set; }
        private IMapper _mapper { get; set; }

        public GameService(ResourceTrackerApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        public async Task<int> CreateGame(GameModel model, CancellationToken cancellationToken)
        {
            if (model.Image != null)
            {
                var fileparam = new FileParameter(model.Image.OpenReadStream(5242880), model.Image.Name, model.Image.ContentType);
                var response = await _apiClient.ApiGameCreateGameWithImageAsync(model.Name, model.Description, model.AltText, fileparam, cancellationToken);
                return response.Id;

            }
            else
            {
                var response = await _apiClient.ApiGameCreateGameAsync(model.Name, model.Description, model.AltText, cancellationToken);
                return response.Id;
            }

        }

        public async Task UpdateGame(GameModel model, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<GameModel, UpdateGameCommand>(model);
            await _apiClient.ApiGameUpdateGameAsync(dto, cancellationToken);
        }

        public async Task DeleteGame(int id, CancellationToken cancellationToken)
        {
            await _apiClient.ApiGameDeleteGameAsync(id, cancellationToken);
        }

        public async Task<GameModel> GetGame(int Id, CancellationToken cancellationToken)
        {
           var response = await _apiClient.ApiGameGetGameAsync(Id, cancellationToken);
           var model = _mapper.Map<GetGameResponse, GameModel>(response);
           return model;
        }

        public async Task<PageableResponseModel<SearchGamesResponseModel>> SearchGame(SearchGamesQueryModel model, CancellationToken cancellationToken)
        {
            var orderDirection = (OrderDirectionEnum?)model.OrderDirection;
            var response = await _apiClient.ApiGameSearchGameAsync(model.GameId, model.Name, model.Description, model.SearchTerms, model.PageNumber,model.PageSize,model.OrderBy, orderDirection, cancellationToken);
            var result = _mapper.Map<PageableResponseModel<SearchGamesResponseModel>>(response);
            return result;
        }
    }
}

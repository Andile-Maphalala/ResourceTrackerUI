
using MapsterMapper;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.Command;
using ResourceTrackerUI.Domain.Models.Feature.Query;

namespace ResourceTrackerUI.Application.Services
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

        public async Task<int> CreateGame(CreateGameCommandModel model, CancellationToken cancellationToken)
        {
            var fileparam = new FileParameter(model.Image.OpenReadStream(), model.Image.Name, model.Image.ContentType);
            var response = await _apiClient.ApiGameCreateGameAsync(model.Name,model.Description,model.AltText, fileparam, cancellationToken);
            return response.Id;
        }

        public async Task UpdateGame(UpdateGameCommandModel model, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UpdateGameCommandModel, UpdateGameCommand>(model);
            await _apiClient.ApiGameUpdateGameAsync(dto, cancellationToken);
        }

        public async Task DeleteGame(int id, CancellationToken cancellationToken)
        {
            await _apiClient.ApiGameDeleteGameAsync(id, cancellationToken);
        }

        public async Task<GetGameResponseModel> GetGame(int Id, CancellationToken cancellationToken)
        {
           var response = await _apiClient.ApiGameGetGameAsync(Id, cancellationToken);
           var model = _mapper.Map<GetGameResponse, GetGameResponseModel>(response);
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

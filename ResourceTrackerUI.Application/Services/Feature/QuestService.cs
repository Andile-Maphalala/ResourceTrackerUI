

using MapsterMapper;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.Quest;
using System.Reflection;

namespace ResourceTrackerUI.Application.Services.Feature
{
    public class QuestService : IQuestService
    {

        private ResourceTrackerApiClient _apiClient { get; set; }
        private IMapper _mapper { get; set; }

        public QuestService(ResourceTrackerApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        public async Task<int> CreateQuest(QuestModel model, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<CreateQuestCommand>(model);
            var result = await _apiClient.ApiQuestCreateQuestAsync(dto, cancellationToken);
            return result.Id;
        }

        public async Task UpdateQuest(QuestModel model, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UpdateQuestCommand>(model);
            await _apiClient.ApiQuestUpdateQuestAsync(dto, cancellationToken);
        }

        public async Task DeleteQuest(int Id, CancellationToken cancellationToken)
        {
            await _apiClient.ApiQuestDeleteQuestAsync(Id, cancellationToken);
        }

        public async Task<QuestModel> GetQuest(int Id, CancellationToken cancellationToken)
        {
            var result = await _apiClient.ApiQuestGetQuestAsync(Id, cancellationToken);
            return _mapper.Map<QuestModel>(result);
        }

        public async Task<PageableResponseModel<SearchQuestsResponseModel>> SearchQuest(SearchQuestsQueryModel query, CancellationToken cancellationToken)
        {
            var orderDirection = (OrderDirectionEnum?)query.OrderDirection;
            var result = await _apiClient.ApiQuestSearchQuestAsync(query.QuestId, query.Name, query.Description, query.Location, query.GameId, query.GameName, query.GameSaveId, query.SearchTerms, query.PageNumber, query.PageSize, query.OrderBy, orderDirection, cancellationToken);
            return _mapper.Map<PageableResponseModel<SearchQuestsResponseModel>>(result);
        }
    }
}

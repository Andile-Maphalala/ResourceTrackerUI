

using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.Quest;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IQuestService
    {
        Task<int> CreateQuest(QuestModel model, CancellationToken cancellationToken);
        Task UpdateQuest(QuestModel model, CancellationToken cancellationToken);
        Task DeleteQuest(int Id, CancellationToken cancellationToken);
        Task<QuestModel> GetQuest(int Id, CancellationToken cancellationToken);
        Task<PageableResponseModel<SearchQuestsResponseModel>> SearchQuest(SearchQuestsQueryModel query, CancellationToken cancellationToken);
    }
}

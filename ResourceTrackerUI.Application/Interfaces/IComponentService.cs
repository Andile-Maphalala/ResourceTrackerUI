
using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.Components;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IComponentService
    {
        Task<int> CreateComponent(ComponentModel model, CancellationToken cancellationToken);
        Task UpdateComponent(ComponentModel model, CancellationToken cancellationToken);
        Task DeleteComponent(int id, CancellationToken cancellationToken);
        Task<ComponentModel> GetComponent(int Id, CancellationToken cancellationToken);
        Task<PageableResponseModel<SearchComponentRequestModel>> SearchComponent(SearchComponentQueryModel model, CancellationToken cancellationToken);
    }
}

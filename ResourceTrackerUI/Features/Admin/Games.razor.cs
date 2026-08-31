using Microsoft.AspNetCore.Components;
using MudBlazor;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Common;
using ResourceTrackerUI.Domain.Enums;
using ResourceTrackerUI.Domain.Models.Common;
using ResourceTrackerUI.Domain.Models.Feature.Game;
using ResourceTrackerUI.Shared.CustomComponents.FormControls;

namespace ResourceTrackerUI.Features.Admin
{
    public partial class Games
    {
        [Inject]
        private IGameService Service { get; set; }
        [Inject]
        private IDialogService dialogService { get; set; }

        private RtTable<SearchGamesResponseModel> tableRef;
        private SearchGamesQueryModel SearchModel { get; set; }


        protected override async Task OnInitializedAsync()
        {
            SearchModel = new SearchGamesQueryModel
            {
                OrderBy = "Id",
                OrderDirection = OrderDirectionEnum.Descending
            };
        }

        private async Task<PageableResponseModel<SearchGamesResponseModel>> LoadData(PageableRequestModel request)
        {
            SearchModel.PageNumber = request.PageNumber;
            SearchModel.PageSize = request.PageSize;
            SearchModel.SearchTerms = request.SearchTerms;
            SearchModel.OrderBy = request.OrderBy;
            SearchModel.OrderDirection = request.OrderDirection;

            var response = await Service.SearchGame(SearchModel, appCancellation.Token);
            return response;
        }

        private async Task Actions(ActionModel<SearchGamesResponseModel> item)
        {
            IDialogReference dialog;
            var model = new GameModel();
            switch (item.Mode)
            {
                case FormModeEnum.Create:
                    item.Item = new SearchGamesResponseModel();
                    break;
                case FormModeEnum.Update:
                case FormModeEnum.View:
                case FormModeEnum.Delete:
                    model = await Service.GetGame(item.Item.Id,appCancellation.Token);
                    break;
                default:
                    return;
            }
            var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
           
            var parameters = new DialogParameters<EditGameModal>
            {
                { x => x.FormMode, item.Mode},
                { x => x.Model, model }
            };

            dialog = await dialogService.ShowAsync<EditGameModal>(item.Mode.ToString(), parameters, options);

            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await tableRef?.RefreshTable();
            }
        }

        private async Task OnSearchChange()
        {
            await tableRef.RefreshTable();
        }
    }
}
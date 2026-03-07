using Microsoft.AspNetCore.Components;
using MudBlazor;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Components.CustomComponents;
using ResourceTrackerUI.Components.PageComponets.Game;
using ResourceTrackerUI.Domain.Common;
using ResourceTrackerUI.Domain.Enums;
using ResourceTrackerUI.Domain.Models.Feature.Command;
using ResourceTrackerUI.Domain.Models.Feature.Query;

namespace ResourceTrackerUI.Pages.Admin
{
    public partial class Game
    {
        [Inject]
        private IGameService Service { get; set; }
        [Inject]
        private IDialogService dialogService { get; set; }

        private RTTable<SearchGamesResponseModel> tableRef;
        private SearchGamesQueryModel Model { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Model = new SearchGamesQueryModel
            {
                OrderBy = "Id",
                OrderDirection = OrderDirectionEnum.Descending
            };
        }

        private async Task<List<SearchGamesResponseModel>> LoadData()
        {
            var response = await Service.SearchGame(Model, appCancellation.Token);
            return response.Data.ToList();
        }

        private async Task Actions(ActionModel<SearchGamesResponseModel> item)
        {


            IDialogReference dialog;

            var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
            var model = new CreateGameCommandModel();
            var parameters = new DialogParameters<CreateGameModal>
            {
                { x => x.FormMode, FormModeEnum.Create},
                { x => x.Model, model }
            };

            dialog = await dialogService.ShowAsync<CreateGameModal>(item.Mode.ToString(), parameters, options);

            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await tableRef?.RefreshTable();
            }
        }
    }
}
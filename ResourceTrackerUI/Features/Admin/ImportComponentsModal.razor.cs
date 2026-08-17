using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ResourceTrackerUI.Application.Common;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Enums;
using ResourceTrackerUI.Domain.Models.Feature.Game;
using ResourceTrackerUI.Domain.Models.Feature.Import;

namespace ResourceTrackerUI.Features.Admin
{
    public partial class ImportComponentsModal
    {

        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        [Inject]
        private IImportService ImportService { get; set; }

        [Inject]
        private IGameService GameService { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        private ImportGameResponseModel _responseModel { get; set; }

        private IBrowserFile _selectedFile;

        private MudFileUpload<IBrowserFile> _fileUpload;

        private SearchGamesResponseModel _selectedGame { get; set; }

        private List<SearchGamesResponseModel> _games = new List<SearchGamesResponseModel>();

        private string _searchString { get; set; } = string.Empty;

        private SearchGamesQueryModel _searchModel { get; set; }

        private bool _disableInput { get; set; } = true;
        private bool _isLoading { get; set; } = false;
        private bool _showClose { get; set; } = false;

        private void Cancel() => MudDialog.Cancel();
        private void Close() => MudDialog.Close();

        protected override async Task OnInitializedAsync()
        {
            _searchModel = new SearchGamesQueryModel
            {
                OrderBy = "Id",
                OrderDirection = OrderDirectionEnum.Descending,
                PageSize = 10
            };
            var response = await GameService.SearchGame(_searchModel, appCancellation.Token);
            _games = response.Data.ToList();

        }

        private async Task Submit()
        {
            try
            {
                _isLoading = true;
                _responseModel = await ImportService.ImportGameComponets(_selectedGame.Id, _selectedFile, appCancellation.Token);
                _isLoading = false;
                if (_responseModel != null)
                {
                    _showClose = true;
                    Snackbar.Add("Import successful!", Severity.Success);
                }
                else
                {
                    Snackbar.Add("Import failed. Please try again.", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                _isLoading = false;
                Snackbar.Add($"An error occurred: {ex.Message}", Severity.Error);
            }
            finally
            {
                _isLoading = false;
            }

            await InvokeAsync(StateHasChanged);
        }

        private async Task<IEnumerable<object>> OnGameDataLoad()
        {
            var data = await GameService.SearchGame(new SearchGamesQueryModel { PageSize = int.MaxValue, OrderBy = "Id" }, appCancellation.Token);
            return data.Data.ToList();
        }

        private async Task<IEnumerable<object>> OnComponentTypeDataLoad()
        {
            var data = EnumHelper.GetEnumSelectList<ComponentTypeEnum>();

            return data;
        }

        private async Task OnGameSearch(string Text)
        {
            _searchModel.PageSize = int.MaxValue;
            _searchModel.SearchTerms = Text;

            var response = await GameService.SearchGame(_searchModel, appCancellation.Token);
            _games = response.Data.ToList();
        }

        private async Task SelectFile()
        {
            if (_selectedFile == null)
            {
                Snackbar.Add("No file selected!", Severity.Warning);
                return;
            }
            if (_selectedFile.Size > 5242880)
            {
                Snackbar.Add("File size exceeds 5MB!", Severity.Error);
                return;
            }
            StateHasChanged();
        }


        private void SelectGame(SearchGamesResponseModel game)
        {
            _selectedGame = game;
            _disableInput = false;
            StateHasChanged();
        }
    }
}
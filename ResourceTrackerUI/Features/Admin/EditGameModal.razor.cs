using Microsoft.AspNetCore.Components;
using MudBlazor;
using ResourceTrackerUI.Application.Common;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Application.Services.Common;
using ResourceTrackerUI.Domain.Enums;
using ResourceTrackerUI.Domain.Models.Feature.Game;
using ApiException = ResourceTrackerUI.ApiClient.ApiException;

namespace ResourceTrackerUI.Features.Admin
{
    public partial class EditGameModal
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        [Inject]
        private IGameService Service { get; set; }

        [Inject]
        private IPictureService PictureService { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Inject]
        private GameSaveState GameSaveState { get; set; }

        [Parameter]
        public GameModel Model { get; set; } = new GameModel();
        [Parameter]
        public FormModeEnum FormMode { get; set; }

        private string _base64 { get; set; }
        private bool _changedPicture { get; set; } = false;

        private string _confirmName = string.Empty;
        private bool _isPurging;

        private bool CanPurge =>
            !_isPurging &&
            !string.IsNullOrEmpty(_confirmName) &&
            _confirmName == Model.Name;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(StateHasChanged);
                _base64 = Model.ImageUrl;
            }
        }
        private async Task Submit()
        {
            switch (FormMode)
            {
                case FormModeEnum.Create:
                    var result = await Service.CreateGame(Model, appCancellation.Token);
                    break;
                case FormModeEnum.Update:
                    await Service.UpdateGame(Model, appCancellation.Token);
                    await PictureService.CrudPicture(FormModeEnum.Update, _changedPicture, Model.PictureId, Model.Image, Model.Name, Model.Id, Model.AltText, (int)ImageUploadTypeEnum.Game, appCancellation.Token);
                    break;
                case FormModeEnum.Delete:
                    await Service.DeleteGame(Model.Id, appCancellation.Token);
                    break;
            }

            MudDialog.Close();
        }

        private async Task Purge()
        {
            if (!CanPurge)
                return;

            _isPurging = true;
            await InvokeAsync(StateHasChanged);

            try
            {
                var result = await Service.PurgeGame(Model.Id, _confirmName, appCancellation.Token);

                if (GameSaveState.ActiveGameId == Model.Id)
                {
                    await GameSaveState.ClearActiveSaveAsync();
                }

                Snackbar.Add(
                    $"Purged '{result.GameName}'. " +
                    $"Components {result.ComponentsDeleted}, recipes {result.RecipesDeleted}, " +
                    $"inventory rows {result.InventoryRowsDeleted}, quests {result.QuestsDeleted}, " +
                    $"saves {result.GameSavesDeleted}, build plans {result.BuildPlansDeleted}, " +
                    $"facilities {result.PlayerFacilitiesDeleted}, images {result.ImageFilesDeleted}.",
                    Severity.Success);

                MudDialog.Close(DialogResult.Ok(true));
            }
            catch (ApiException ex)
            {
                Snackbar.Add(ApiExceptionHelper.GetMessage(ex), Severity.Error);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Purge failed: {ex.Message}", Severity.Error);
            }
            finally
            {
                _isPurging = false;
            }
        }

        private void Cancel() => MudDialog.Cancel();

        private async Task SwapPicture()
        {
            if (Model.Image == null)
            {
                Snackbar.Add("No file selected!", Severity.Warning);
                return;
            }
            if (Model.Image.Size > 5 * 1024 * 1024)
            {
                Snackbar.Add("File size exceeds 5MB!", Severity.Error);
                return;
            }
            StateHasChanged();
            using var stream = Model.Image.OpenReadStream(5242880); // Limit to 5MB.
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            _base64 = $"data:{Model.Image.ContentType};base64,{Convert.ToBase64String(fileBytes)}";
            _changedPicture = true;
            Snackbar.Add("Image updated successfully!", Severity.Success);
        }

        private void ClearImage()
        {
            Model.Image = null;
            _base64 = null;
            _changedPicture = true;

        }
    }
}
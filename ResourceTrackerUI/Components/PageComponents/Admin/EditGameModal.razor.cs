using Microsoft.AspNetCore.Components;
using MudBlazor;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Enums;
using ResourceTrackerUI.Domain.Models.Feature.Game;

namespace ResourceTrackerUI.Components.PageComponents.Admin
{
    public partial class EditGameModal
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        [Inject]
        private IGameService Service { get; set; }
        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Parameter]
        public GameModel Model { get; set; } = new GameModel();
        [Parameter]
        public FormModeEnum FormMode { get; set; }

        private bool Swapping { get; set; } = true;
        private string Base64 { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(StateHasChanged);
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
                    break;
                case FormModeEnum.Delete:
                    await Service.DeleteGame(Model.Id, appCancellation.Token);
                    break;
            }

            MudDialog.Close();
        }

        private void Cancel() => MudDialog.Cancel();

        private async Task SwapPicture()
        {
            if (Model.Image == null)
            {
                Snackbar.Add("No file selected!", Severity.Warning);
                return;
            }
            if (Model.Image.Size > 5242880)
            {
                Snackbar.Add("File size exceeds 5MB!", Severity.Error);
                return;
            }
            Swapping = true;
            StateHasChanged();
            using var stream = Model.Image.OpenReadStream(5242880); // Limit to 5MB.
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            Base64 = $"data:{Model.Image.ContentType};base64,{Convert.ToBase64String(fileBytes)}";
            Snackbar.Add("Image updated successfully!", Severity.Success);

            Swapping = false;
        }
    }
}
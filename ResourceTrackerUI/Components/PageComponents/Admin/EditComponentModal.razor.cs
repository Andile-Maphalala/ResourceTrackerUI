using Microsoft.AspNetCore.Components;
using MudBlazor;
using ResourceTrackerUI.Application.Common;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Enums;
using ResourceTrackerUI.Domain.Models.Feature.Components;
using ResourceTrackerUI.Domain.Models.Feature.Game;

namespace ResourceTrackerUI.Components.PageComponents.Admin
{
    public partial class EditComponentModal
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        [Inject]
        private IComponentService Service { get; set; }

        [Inject]
        private IGameService GameService { get; set; }

        [Inject]
        private IPictureService PictureService { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Parameter]
        public ComponentModel Model { get; set; } = new ComponentModel();

        [Parameter]
        public FormModeEnum FormMode { get; set; }

        private string _base64 { get; set; }
        private bool _changedPicture { get; set; } = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _base64 = Model.ImageUrl;
                await InvokeAsync(StateHasChanged);
            }
        }
        private async Task Submit()
        {
            switch (FormMode)
            {
                case FormModeEnum.Create:
                    var result = await Service.CreateComponent(Model, appCancellation.Token);
                    await PictureService.CrudPicture(FormModeEnum.Create, _changedPicture, Model.PictureId, Model.Image, Model.Name, result, Model.AltText, (int)ImageUploadTypeEnum.Component, appCancellation.Token);
                    break;
                case FormModeEnum.Update:
                    await Service.UpdateComponent(Model, appCancellation.Token);
                    await PictureService.CrudPicture(FormModeEnum.Update, _changedPicture, Model.PictureId, Model.Image, Model.Name, Model.Id, Model.AltText, (int)ImageUploadTypeEnum.Component, appCancellation.Token);
                    break;
                case FormModeEnum.Delete:
                    await Service.DeleteComponent(Model.Id, appCancellation.Token);
                    break;
            }

            MudDialog.Close();
        }

        private void Cancel() => MudDialog.Cancel();

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
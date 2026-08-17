using Microsoft.AspNetCore.Components;
using MudBlazor;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Enums;
using ResourceTrackerUI.Domain.Models.Feature.BuildPlan;

namespace ResourceTrackerUI.Components.PageComponents.feature.BuildPlan
{
    public partial class EditBuildPlanModal
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        [Inject]
        private IBuildPlanService Service { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Parameter]
        public BuildPlanModel Model { get; set; } = new BuildPlanModel();
        [Parameter]
        public FormModeEnum FormMode { get; set; }

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
                    await Service.CreateBuildPlan(Model, appCancellation.Token);
                    break;
                case FormModeEnum.Update:
                    await Service.UpdateBuildPlan(Model, appCancellation.Token);
                    break;
                case FormModeEnum.Delete:
                    await Service.DeleteBuildPlan(Model.Id, appCancellation.Token);
                    break;
            }

            MudDialog.Close();
        }

        private void Cancel() => MudDialog.Cancel();
    }
}
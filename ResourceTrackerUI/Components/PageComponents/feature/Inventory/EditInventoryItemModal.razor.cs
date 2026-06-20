using Microsoft.AspNetCore.Components;
using MudBlazor;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Application.Services.Common;
using ResourceTrackerUI.Domain.Enums;
using ResourceTrackerUI.Domain.Models.Feature.Components;
using ResourceTrackerUI.Domain.Models.Feature.Inventory;

namespace ResourceTrackerUI.Components.PageComponents.feature.Inventory
{
    public partial class EditInventoryItemModal
    {
        [CascadingParameter] 
        private IMudDialogInstance MudDialog { get; set; }
        [Inject] 
        private IInventoryService InventoryService { get; set; }
        [Inject] 
        private IComponentService ComponentService { get; set; }
        [Inject] 
        private ISnackbar Snackbar { get; set; }
        [Inject] 
        private GameSaveState GameSaveState { get; set; }

        [Parameter] 
        public InventoryModel Model { get; set; } = new();
        [Parameter] 
        public FormModeEnum FormMode { get; set; }

        private SearchComponentQueryModel _searchModel { get; set; }
        private bool _disableDropdown { get; set; } = false;

        private async Task Submit()
        {
            switch (FormMode)
            {
                case FormModeEnum.Create: 
                    await InventoryService.CreateInventory(Model, appCancellation.Token); break;
                case FormModeEnum.Update: 
                    await InventoryService.UpdateInventory(Model, appCancellation.Token); break;
                case FormModeEnum.Delete: 
                    await InventoryService.DeleteInventory(Model.Id, appCancellation.Token); break;
            }
            MudDialog.Close();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if(FormMode == FormModeEnum.Update)
            {
                _disableDropdown = true;

            }
            _searchModel = new SearchComponentQueryModel
            {
                PageSize = int.MaxValue,
                OrderBy = "Name",
                GameId = GameSaveState.ActiveGameId
            };
        }

        private void Cancel() => MudDialog.Cancel();

        private async Task<IEnumerable<object>> OnComponentDataLoad()
        {
            var data = await ComponentService.SearchComponent(_searchModel, appCancellation.Token);
            return data.Data.ToList();
        }
    }
}
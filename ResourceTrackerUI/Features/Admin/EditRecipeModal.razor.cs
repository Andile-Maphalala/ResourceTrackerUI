using Microsoft.AspNetCore.Components;
using MudBlazor;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Common;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Enums;
using ResourceTrackerUI.Domain.Models.Feature.Components;
using ResourceTrackerUI.Domain.Models.Feature.Recipe;
using DomainOrderDirection = ResourceTrackerUI.Domain.Enums.OrderDirectionEnum;

namespace ResourceTrackerUI.Features.Admin
{
    public partial class EditRecipeModal
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }

        [Inject]
        private IRecipeService RecipeService { get; set; }

        [Inject]
        private IComponentService ComponentService { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Parameter]
        public RecipeModel Model { get; set; } = new RecipeModel();

        [Parameter]
        public FormModeEnum FormMode { get; set; }

        [Parameter]
        public int GameId { get; set; }

        // A game can hold thousands of components; loading them all into a select freezes the
        // browser. Load a page up front and search the rest through the select's search box.
        private const int ComponentPageSize = 100;

        private List<SearchComponentResponseModel> _ingredientOptions = new();
        private List<SearchComponentResponseModel> _facilityOptions = new();

        private bool IsEditable =>
            FormMode == FormModeEnum.Create || FormMode == FormModeEnum.Update;

        private bool HasStation => Model.RequiredFacilityId.HasValue;

        protected override async Task OnInitializedAsync()
        {
            Model.Ingredients ??= new List<RecipeIngredientModel>();

            _ingredientOptions = await SearchComponents(string.Empty, null);
            _facilityOptions = await SearchComponents(string.Empty, ComponentTypeEnum.Facility);

            // When editing, the selected components are probably not in the first page - keep
            // them in the list so the selects can show their names.
            foreach (var ingredient in Model.Ingredients)
            {
                WithSelected(_ingredientOptions, ingredient.ComponentId, ingredient.ComponentName);
            }

            WithSelected(_facilityOptions, Model.RequiredFacilityId, Model.RequiredFacilityName);
        }

        private async Task<List<SearchComponentResponseModel>> SearchComponents(string term, ComponentTypeEnum? type)
        {
            try
            {
                var data = await ComponentService.SearchComponent(new SearchComponentQueryModel
                {
                    GameId = GameId,
                    SearchTerms = term,
                    Type = type.HasValue ? (int)type.Value : null,
                    PageSize = ComponentPageSize,
                    OrderBy = "Name",
                    OrderDirection = DomainOrderDirection.Ascending
                }, appCancellation.Token);

                // A component can never be an ingredient of its own recipe, so leave it out of
                // the picker rather than relying on the API to reject it.
                return data.Data.Where(c => c.Id != Model.ComponentId).ToList();
            }
            catch (ApiException ex)
            {
                Snackbar.Add(ApiExceptionHelper.GetMessage(ex), Severity.Error);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Could not load components: {ex.Message}", Severity.Error);
            }

            return new List<SearchComponentResponseModel>();
        }

        private static List<SearchComponentResponseModel> WithSelected(
            List<SearchComponentResponseModel> options, int? componentId, string? componentName)
        {
            if (!componentId.HasValue || componentId.Value <= 0 || options.Any(c => c.Id == componentId.Value))
                return options;

            options.Insert(0, new SearchComponentResponseModel
            {
                Id = componentId.Value,
                Name = componentName ?? componentId.Value.ToString()
            });

            return options;
        }

        private async Task Submit()
        {
            if (FormMode == FormModeEnum.Delete)
            {
                await DeleteAsync();
                return;
            }

            if (!IsEditable)
            {
                MudDialog.Close();
                return;
            }

            if (!Validate())
                return;

            try
            {
                if (FormMode == FormModeEnum.Create)
                {
                    await RecipeService.CreateRecipe(Model, appCancellation.Token);
                }
                else
                {
                    await RecipeService.UpdateRecipe(Model, appCancellation.Token);
                }

                Snackbar.Add("Recipe saved.", Severity.Success);
                MudDialog.Close(DialogResult.Ok(true));
            }
            catch (ApiException ex)
            {
                Snackbar.Add(ApiExceptionHelper.GetMessage(ex), Severity.Error);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Could not save the recipe: {ex.Message}", Severity.Error);
            }
        }

        private async Task DeleteAsync()
        {
            try
            {
                await RecipeService.DeleteRecipe(Model.Id, appCancellation.Token);
                Snackbar.Add("Recipe deleted.", Severity.Success);
                MudDialog.Close(DialogResult.Ok(true));
            }
            catch (ApiException ex)
            {
                Snackbar.Add(ApiExceptionHelper.GetMessage(ex), Severity.Error);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Could not delete the recipe: {ex.Message}", Severity.Error);
            }
        }

        private bool Validate()
        {
            if (Model.AmountMade < 1)
            {
                Snackbar.Add("Amount made must be greater than 0.", Severity.Warning);
                return false;
            }

            // The API keeps the level at 1 when there is no station; mirror that so the value
            // shown is the value that gets stored.
            if (!Model.RequiredFacilityId.HasValue)
            {
                Model.RequiredFacilityLevel = 1;
            }
            else if (Model.RequiredFacilityLevel < 1)
            {
                Snackbar.Add("Station level cannot be less than 1.", Severity.Warning);
                return false;
            }

            var ingredientIds = Model.Ingredients.Select(i => i.ComponentId).ToList();

            if (ingredientIds.Any(id => id <= 0))
            {
                Snackbar.Add("Choose a component for every ingredient.", Severity.Warning);
                return false;
            }

            if (ingredientIds.Contains(Model.ComponentId))
            {
                Snackbar.Add("A recipe cannot require its own output.", Severity.Warning);
                return false;
            }

            if (ingredientIds.Distinct().Count() != ingredientIds.Count)
            {
                Snackbar.Add("The same ingredient is listed more than once.", Severity.Warning);
                return false;
            }

            if (Model.Ingredients.Any(i => i.AmountRequired < 1))
            {
                Snackbar.Add("Ingredient amounts must be greater than 0.", Severity.Warning);
                return false;
            }

            return true;
        }

        private void AddIngredient()
        {
            var used = Model.Ingredients.Select(i => i.ComponentId).ToHashSet();
            var next = _ingredientOptions.FirstOrDefault(c => !used.Contains(c.Id));

            Model.Ingredients.Add(new RecipeIngredientModel
            {
                ComponentId = next?.Id ?? 0,
                ComponentName = next?.Name,
                AmountRequired = 1
            });
        }

        private void RemoveIngredient(RecipeIngredientModel ingredient)
        {
            Model.Ingredients.Remove(ingredient);
        }

        private void OnFacilityChanged()
        {
            if (!Model.RequiredFacilityId.HasValue || Model.RequiredFacilityLevel < 1)
            {
                Model.RequiredFacilityLevel = 1;
            }
        }

        private Task<IEnumerable<object>> LoadIngredientOptions()
        {
            return Task.FromResult<IEnumerable<object>>(_ingredientOptions);
        }

        private Task<IEnumerable<object>> LoadFacilityOptions()
        {
            return Task.FromResult<IEnumerable<object>>(_facilityOptions);
        }

        private async Task<IEnumerable<object>> SearchIngredientOptions(string term, RecipeIngredientModel ingredient)
        {
            var results = await SearchComponents(term, null);
            return WithSelected(results, ingredient.ComponentId, ingredient.ComponentName);
        }

        private async Task<IEnumerable<object>> SearchFacilityOptions(string term)
        {
            var results = await SearchComponents(term, ComponentTypeEnum.Facility);
            return WithSelected(results, Model.RequiredFacilityId, Model.RequiredFacilityName);
        }

        private void Cancel() => MudDialog.Cancel();
    }
}

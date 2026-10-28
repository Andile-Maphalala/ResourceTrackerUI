using Microsoft.AspNetCore.Components;
using MudBlazor;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Common;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Enums;
using ResourceTrackerUI.Domain.Models.Feature.Components;
using ResourceTrackerUI.Domain.Models.Feature.Game;
using ResourceTrackerUI.Domain.Models.Feature.Recipe;
using ResourceTrackerUI.Shared.CustomComponents.FormControls;
using DomainOrderDirection = ResourceTrackerUI.Domain.Enums.OrderDirectionEnum;

namespace ResourceTrackerUI.Features.Admin
{
    public partial class Recipes
    {
        [Inject]
        private IRecipeService RecipeService { get; set; }

        [Inject]
        private IComponentService ComponentService { get; set; }

        [Inject]
        private IGameService GameService { get; set; }

        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        private int? _gameId;
        private int? _componentId;
        private string _componentName = string.Empty;
        private List<SearchComponentResponseModel> _componentOptions = new();
        private List<RecipeModel> _recipes = new();
        private bool _loadingRecipes;

        private async Task<IEnumerable<object>> LoadGameOptions()
        {
            var data = await GameService.SearchGame(new SearchGamesQueryModel
            {
                PageSize = int.MaxValue,
                OrderBy = "Id",
                OrderDirection = DomainOrderDirection.Descending
            }, appCancellation.Token);

            return data.Data.ToList();
        }

        // A game can hold thousands of components; loading them all into the select is what made
        // it freeze. Load a page and let the search box query the rest.
        private const int ComponentPageSize = 100;

        private Task<IEnumerable<object>> LoadComponentOptions() => LoadComponents(string.Empty);

        private Task<IEnumerable<object>> SearchComponentOptions(string term) => LoadComponents(term);

        private async Task<IEnumerable<object>> LoadComponents(string term)
        {
            if (!_gameId.HasValue)
            {
                _componentOptions = new List<SearchComponentResponseModel>();
                return new List<object>();
            }

            try
            {
                var data = await ComponentService.SearchComponent(new SearchComponentQueryModel
                {
                    GameId = _gameId,
                    Name = term,
                    PageSize = ComponentPageSize,
                    OrderBy = "Name",
                    OrderDirection = DomainOrderDirection.Ascending
                }, appCancellation.Token);

                _componentOptions = data.Data.ToList();
                return _componentOptions;
            }
            catch (ApiException ex)
            {
                Snackbar.Add(ApiExceptionHelper.GetMessage(ex), Severity.Error);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Could not load components: {ex.Message}", Severity.Error);
            }

            _componentOptions = new List<SearchComponentResponseModel>();
            return _componentOptions;
        }

        private void OnGameChanged()
        {
            _componentId = null;
            _componentName = string.Empty;
            _recipes = new List<RecipeModel>();

            // The component select is keyed on the game, so it is rebuilt and reloads its options.
        }

        private async Task OnComponentChanged()
        {
            _recipes = new List<RecipeModel>();

            if (!_componentId.HasValue)
            {
                _componentName = string.Empty;
                return;
            }

            _componentName = _componentOptions.FirstOrDefault(c => c.Id == _componentId.Value)?.Name ?? string.Empty;
            await LoadRecipes();
        }

        private async Task LoadRecipes()
        {
            if (!_componentId.HasValue)
                return;

            _loadingRecipes = true;

            try
            {
                _recipes = await RecipeService.GetRecipesForComponent(_componentId.Value, appCancellation.Token);
            }
            catch (ApiException ex)
            {
                _recipes = new List<RecipeModel>();
                Snackbar.Add(ApiExceptionHelper.GetMessage(ex), Severity.Error);
            }
            catch (Exception ex)
            {
                _recipes = new List<RecipeModel>();
                Snackbar.Add($"Could not load recipes: {ex.Message}", Severity.Error);
            }
            finally
            {
                _loadingRecipes = false;
            }
        }

        private async Task OpenCreate()
        {
            if (!_componentId.HasValue)
                return;

            var model = new RecipeModel
            {
                ComponentId = _componentId.Value,
                ComponentName = _componentName,
                AmountMade = 1,
                RequiredFacilityLevel = 1
            };

            await OpenModal(model, FormModeEnum.Create);
        }

        private async Task OpenAction(RecipeModel recipe, FormModeEnum mode)
        {
            try
            {
                var model = await RecipeService.GetRecipe(recipe.Id, appCancellation.Token);
                await OpenModal(model, mode);
            }
            catch (ApiException ex)
            {
                Snackbar.Add(ApiExceptionHelper.GetMessage(ex), Severity.Error);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Could not load the recipe: {ex.Message}", Severity.Error);
            }
        }

        private async Task OpenModal(RecipeModel model, FormModeEnum mode)
        {
            var parameters = new DialogParameters<EditRecipeModal>
            {
                { x => x.Model, model },
                { x => x.FormMode, mode },
                { x => x.GameId, _gameId ?? 0 }
            };

            var options = new DialogOptions
            {
                CloseOnEscapeKey = true,
                FullWidth = true,
                MaxWidth = MaxWidth.Medium
            };

            var dialog = await DialogService.ShowAsync<EditRecipeModal>(mode.ToString(), parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await LoadRecipes();
            }
        }

        private static string StationText(RecipeModel recipe)
        {
            if (!recipe.RequiredFacilityId.HasValue)
                return "None";

            var name = recipe.RequiredFacilityName ?? recipe.RequiredFacilityId.Value.ToString();
            return recipe.RequiredFacilityLevel > 1 ? $"{name} (Lvl {recipe.RequiredFacilityLevel})" : name;
        }
    }
}

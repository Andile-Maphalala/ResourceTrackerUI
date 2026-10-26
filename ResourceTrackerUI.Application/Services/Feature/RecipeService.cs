using MapsterMapper;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models.Feature.Recipe;

namespace ResourceTrackerUI.Application.Services.Feature
{
    public class RecipeService : IRecipeService
    {
        private ResourceTrackerApiClient _apiClient { get; set; }
        private IMapper _mapper { get; set; }

        public RecipeService(ResourceTrackerApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        public async Task<List<RecipeModel>> GetRecipesForComponent(int componentId, CancellationToken cancellationToken)
        {
            var response = await _apiClient.ApiRecipeGetRecipesForComponentAsync(componentId, cancellationToken);
            var result = _mapper.Map<List<RecipeModel>>(response) ?? new List<RecipeModel>();

            foreach (var recipe in result)
            {
                recipe.Ingredients ??= new List<RecipeIngredientModel>();
            }

            return result;
        }

        public async Task<RecipeModel> GetRecipe(int id, CancellationToken cancellationToken)
        {
            var response = await _apiClient.ApiRecipeGetRecipeAsync(id, cancellationToken);
            var result = _mapper.Map<RecipeModel>(response);
            result.Ingredients ??= new List<RecipeIngredientModel>();
            return result;
        }

        public async Task<int> CreateRecipe(RecipeModel model, CancellationToken cancellationToken)
        {
            var dto = new CreateRecipeCommand
            {
                ComponentId = model.ComponentId,
                AmountMade = model.AmountMade,
                RequiredFacilityId = model.RequiredFacilityId,
                RequiredFacilityLevel = model.RequiredFacilityLevel,
                Ingredients = ToIngredients(model)
            };

            var response = await _apiClient.ApiRecipeCreateRecipeAsync(dto, cancellationToken);
            return response.Id;
        }

        public async Task UpdateRecipe(RecipeModel model, CancellationToken cancellationToken)
        {
            var dto = new UpdateRecipeCommand
            {
                Id = model.Id,
                ComponentId = model.ComponentId,
                AmountMade = model.AmountMade,
                RequiredFacilityId = model.RequiredFacilityId,
                RequiredFacilityLevel = model.RequiredFacilityLevel,
                Ingredients = ToIngredients(model)
            };

            await _apiClient.ApiRecipeUpdateRecipeAsync(dto, cancellationToken);
        }

        private static List<RecipeIngredientDto> ToIngredients(RecipeModel model)
        {
            return model.Ingredients
                .Select(i => new RecipeIngredientDto
                {
                    ComponentId = i.ComponentId,
                    AmountRequired = i.AmountRequired
                })
                .ToList();
        }

        public async Task DeleteRecipe(int id, CancellationToken cancellationToken)
        {
            await _apiClient.ApiRecipeDeleteRecipeAsync(id, cancellationToken);
        }
    }
}

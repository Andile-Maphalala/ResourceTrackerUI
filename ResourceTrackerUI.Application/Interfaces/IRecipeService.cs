using ResourceTrackerUI.Domain.Models.Feature.Recipe;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IRecipeService
    {
        Task<List<RecipeModel>> GetRecipesForComponent(int componentId, CancellationToken cancellationToken);
        Task<RecipeModel> GetRecipe(int id, CancellationToken cancellationToken);
        Task<int> CreateRecipe(RecipeModel model, CancellationToken cancellationToken);
        Task UpdateRecipe(RecipeModel model, CancellationToken cancellationToken);
        Task DeleteRecipe(int id, CancellationToken cancellationToken);
    }
}

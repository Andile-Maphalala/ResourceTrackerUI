using ResourceTrackerUI.Domain.Attributes;

namespace ResourceTrackerUI.Domain.Models.Feature.Recipe
{
    public class RecipeIngredientModel
    {
        [FormField("Ingredient")]
        public int ComponentId { get; set; }
        public string? ComponentName { get; set; }
        public int ComponentType { get; set; }
        public string? ComponentTypeName { get; set; }
        [FormField("Amount")]
        public int AmountRequired { get; set; } = 1;
        public string? ComponentImageUrl { get; set; }
    }
}

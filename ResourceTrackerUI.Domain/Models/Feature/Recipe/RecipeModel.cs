using ResourceTrackerUI.Domain.Attributes;

namespace ResourceTrackerUI.Domain.Models.Feature.Recipe
{
    public class RecipeModel
    {
        public int Id { get; set; }
        public int ComponentId { get; set; }

        [FormField("Output")]
        public string? ComponentName { get; set; }

        public string? ComponentImageUrl { get; set; }

        [FormField("Amount Made")]
        public int AmountMade { get; set; } = 1;

        [FormField("Station")]
        public int? RequiredFacilityId { get; set; }

        public string? RequiredFacilityName { get; set; }
        public string? RequiredFacilityImageUrl { get; set; }

        [FormField("Station Level")]
        public int RequiredFacilityLevel { get; set; } = 1;

        public List<RecipeIngredientModel> Ingredients { get; set; } = new();
    }
}

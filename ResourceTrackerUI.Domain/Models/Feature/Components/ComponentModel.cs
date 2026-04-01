using ResourceTrackerUI.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ResourceTrackerUI.Domain.Models.Feature.Components
{
    public class ComponentModel
    {
        [FormField("Component Id")]
        public int Id { get; set; }

        [Required]
        [FormField("Name", MaxLength = 100)]
        public string Name { get; set; }

        [FormField("Description",MaxLength = 225)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [FormField("Component Type")]
        public int? Type { get; set; }

        [FormField("Component Type")]
        public string TypeName { get; set; }

        [Required]
        [FormField("Game")]
        public int? GameId { get; set; }

        [FormField("Game")]
        public string GameName { get; set; }
    }
}

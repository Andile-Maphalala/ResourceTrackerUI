using Microsoft.AspNetCore.Components.Forms;
using ResourceTrackerUI.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ResourceTrackerUI.Domain.Models.Feature.Game
{
    public class GameModel
    {
        public int Id { get; set; }

        [Required]
        [FormField(DisplayName = "Name", MaxLength = 100)]
        public string Name { get; set; }

        [FormField(DisplayName = "Description", MaxLength = 225)]
        public string? Description { get; set; } = string.Empty;

        [FormField(DisplayName = "Alt Text", MaxLength = 100)]
        public string? AltText { get; set; } = string.Empty;

        public IBrowserFile? Image { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
        public int? PictureId { get; set; }
    }
}

using Microsoft.AspNetCore.Components.Forms;

namespace ResourceTrackerUI.Domain.Models.Feature.Command
{
    public class CreateGameCommandModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? AltText { get; set; }
        public IBrowserFile? Image { get; set; }
    }
}

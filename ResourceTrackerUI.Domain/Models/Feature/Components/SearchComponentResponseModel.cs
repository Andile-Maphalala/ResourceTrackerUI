
using ResourceTrackerUI.Domain.Attributes;

namespace ResourceTrackerUI.Domain.Models.Feature.Components
{
    public class SearchComponentResponseModel
    {
        [CustomColumn(Visible = false, Order = 1)]
        public int Id { get; set; }

        [CustomColumn(Visible = true, Order = 2)]
        public string Name { get; set; }

        [CustomColumn(Visible = true, Order = 3)]
        public string Description { get; set; }

        public int Type { get; set; }

        [CustomColumn(Visible = true, Order = 5, DisplayName = "Component Type")]
        public string TypeName { get; set; }


        [CustomColumn(Visible = true, Order = 6, DisplayName = "Image Url")]
        public string? ImageUrl { get; set; }
    }
}

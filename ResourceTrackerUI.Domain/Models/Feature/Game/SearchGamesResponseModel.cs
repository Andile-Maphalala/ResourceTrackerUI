using ResourceTrackerUI.Domain.Attributes;

namespace ResourceTrackerUI.Domain.Models.Feature.Game
{
    public class SearchGamesResponseModel
    {
        [CustomColumn(Visible = true, Order = 1)]
        public int Id { get; set; }
        [CustomColumn(Visible = true, Order = 2)]
        public string Name { get; set; }
        [CustomColumn(Visible = true, Order = 3)]
        public string? Description { get; set; }
        [CustomColumn(Visible = true, Order = 4,Searchable = false,Sortable = false)]
        public string? ImageUrl { get; set; }
    }
}

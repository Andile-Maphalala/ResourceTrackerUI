namespace ResourceTrackerUI.Domain.Models.Feature.Components
{
    public class ComponentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Type { get; set; }
        public int GameId { get; set; }
    }
}

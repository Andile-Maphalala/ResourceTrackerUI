

using ResourceTrackerUI.Domain.Attributes;

namespace ResourceTrackerUI.Domain.Models.Feature.BuildPlan
{
    public class SearchBuildPlansResponseModel
    {
        [CustomColumn(Visible = true, Order = 0)]
        public int Id { get; set; }

        [CustomColumn(Visible = true, Order = 1)]
        public string Name { get; set; }

        [CustomColumn(Visible = true, Order = 2)]
        public string Description { get; set; }
        public int GameSaveId { get; set; }
        public string GameSaveName { get; set; }
        public int GameId { get; set; }
        public string GameName { get; set; }
    }
}

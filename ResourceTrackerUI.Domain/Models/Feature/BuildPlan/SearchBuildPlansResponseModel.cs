

namespace ResourceTrackerUI.Domain.Models.Feature.BuildPlan
{
    public class SearchBuildPlansResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int GameSaveId { get; set; }
        public string GameSaveName { get; set; }
        public int GameId { get; set; }
        public string GameName { get; set; }
    }
}



namespace ResourceTrackerUI.Domain.Models.Feature.BuildPlanQuest
{
    public class GetBuildPlanQuestQueryModel
    {
        public int BuildPlanId { get; set; }
        public string BuildPlanName { get; set; }
        public int QuestId { get; set; }
        public int GameSaveId { get; set; }
    }
}



namespace ResourceTrackerUI.Domain.Models.Feature.BuildPlanQuest
{
    public class GetBuildPlanQuestListResponseModel
    {
        public int BuildPlanId { get; set; }
        public string BuildPlanName { get; set; }
        public string BuildPlanDescription { get; set; }
        public int QuestId { get; set; }
        public string QuestName { get; set; }
        public string QuestDescription { get; set; }
    }
}

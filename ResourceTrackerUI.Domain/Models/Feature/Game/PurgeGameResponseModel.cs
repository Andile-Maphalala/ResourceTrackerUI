namespace ResourceTrackerUI.Domain.Models.Feature.Game
{
    public class PurgeGameResponseModel
    {
        public int GameId { get; set; }
        public string GameName { get; set; } = string.Empty;
        public int ComponentsDeleted { get; set; }
        public int RecipesDeleted { get; set; }
        public int RecipeComponentsDeleted { get; set; }
        public int InventoryRowsDeleted { get; set; }
        public int PlayerFacilitiesDeleted { get; set; }
        public int QuestsDeleted { get; set; }
        public int GameSavesDeleted { get; set; }
        public int BuildPlansDeleted { get; set; }
        public int BuildPlanComponentsDeleted { get; set; }
        public int BuildPlanQuestsDeleted { get; set; }
        public int PicturesDeleted { get; set; }
        public int ImageFilesDeleted { get; set; }
    }
}

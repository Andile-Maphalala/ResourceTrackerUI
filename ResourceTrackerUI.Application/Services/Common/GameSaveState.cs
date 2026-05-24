

using ResourceTrackerUI.Application.Interfaces;

namespace ResourceTrackerUI.Application.Services.Common
{
    public class GameSaveState(ILocalStorageService localStorageService)
    {
        public int? ActiveGameSaveId { get; private set; }
        public int? ActiveGameId { get; private set; }
        public string? ActiveGameName { get; private set; }
        public string? ActiveSaveName { get; private set; }
        public string? ActiveGameImageUrl { get; private set; }
        public bool HasActiveSave => ActiveGameSaveId.HasValue;

        public event Action? OnChange;

        public async Task SetActiveSave(int id,int gameId, string gameName, string saveName, string? gameImageUrl = null)
        {
            ActiveGameSaveId = id;
            ActiveGameId = gameId;
            ActiveGameName = gameName;
            ActiveSaveName = saveName;
            ActiveGameImageUrl = gameImageUrl;
            await PersistToStorage();
            OnChange?.Invoke();
        }

        public void ClearActiveSave()
        {
            ActiveGameSaveId = null;
            ActiveGameId = null;
            ActiveGameName = null;
            ActiveSaveName = null;
            ActiveGameImageUrl = null;
            OnChange?.Invoke();
        }

        // Call this on app start to reload from localStorage
        public async Task LoadFromStorage()
        {
            var storage = await localStorageService.GetItemAsync<GameSaveStorageModel>("activeGameSave");
            if (storage != null)
            {
                ActiveGameSaveId = storage.Id;
                ActiveGameId = storage.GameId;
                ActiveGameName = storage.GameName;
                ActiveSaveName = storage.SaveName;
                ActiveGameImageUrl = storage.GameImageUrl;
                OnChange?.Invoke();
            }
        }

        private async Task PersistToStorage()
        {
            var data = new GameSaveStorageModel
            {
                Id = ActiveGameSaveId,
                GameId = ActiveGameId,
                GameName = ActiveGameName ?? string.Empty,
                SaveName = ActiveSaveName ?? string.Empty,
                GameImageUrl = ActiveGameImageUrl
            };
            await localStorageService.SetItemAsync("activeGameSave", data);
        }

        private class GameSaveStorageModel
        {
            public int? Id { get; set; }
            public int? GameId { get; set; }
            public string GameName { get; set; } = string.Empty;
            public string SaveName { get; set; } = string.Empty;
            public string? GameImageUrl { get; set; }
        }

    }

}

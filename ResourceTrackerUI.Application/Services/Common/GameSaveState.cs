using ResourceTrackerUI.Application.Interfaces;

namespace ResourceTrackerUI.Application.Services.Common
{
    public class GameSaveState(ILocalStorageService localStorageService)
    {
        private const string StorageKey = "activeGameSave";

        private Task? _initialization;

        public int? ActiveGameSaveId { get; private set; }
        public int? ActiveGameId { get; private set; }
        public string? ActiveGameName { get; private set; }
        public string? ActiveSaveName { get; private set; }
        public string? ActiveGameImageUrl { get; private set; }
        public bool HasActiveSave => ActiveGameSaveId.HasValue;

        public event Action? OnChange;

        /// <summary>
        /// Loads the active save from localStorage. Idempotent, so it is safe to call from
        /// multiple components. App.razor awaits this before rendering the router so pages
        /// never query the API with an empty save id after a hard refresh.
        /// </summary>
        public Task EnsureInitializedAsync() => _initialization ??= LoadFromStorageAsync();

        public async Task SetActiveSave(int id, int gameId, string gameName, string saveName, string? gameImageUrl = null)
        {
            ActiveGameSaveId = id;
            ActiveGameId = gameId;
            ActiveGameName = gameName;
            ActiveSaveName = saveName;
            ActiveGameImageUrl = gameImageUrl;
            await PersistToStorage();
            OnChange?.Invoke();
        }

        /// <summary>
        /// Clears the in-memory state and removes the persisted entry, so a refresh does not
        /// resurrect a save that has been deleted or switched away from.
        /// </summary>
        public async Task ClearActiveSaveAsync()
        {
            ResetState();
            await localStorageService.RemoveItemAsync(StorageKey);
            OnChange?.Invoke();
        }

        private async Task LoadFromStorageAsync()
        {
            try
            {
                var storage = await localStorageService.GetItemAsync<GameSaveStorageModel>(StorageKey);
                if (storage?.Id is null)
                {
                    return;
                }

                ActiveGameSaveId = storage.Id;
                ActiveGameId = storage.GameId;
                ActiveGameName = storage.GameName;
                ActiveSaveName = storage.SaveName;
                ActiveGameImageUrl = storage.GameImageUrl;
                OnChange?.Invoke();
            }
            catch (Exception)
            {
                // A corrupt entry must not stop the app from starting - fall back to no active save.
                ResetState();
            }
        }

        private void ResetState()
        {
            ActiveGameSaveId = null;
            ActiveGameId = null;
            ActiveGameName = null;
            ActiveSaveName = null;
            ActiveGameImageUrl = null;
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
            await localStorageService.SetItemAsync(StorageKey, data);
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

using Mapster;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Domain.Models.Feature.GameSave;

namespace ResourceTrackerUI.Application.Mappings
{
    public class GameSaveMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetGameSaveListResponse, GameSaveListModel>()
                .Map(dest => dest.Created, src => src.Created.DateTime);

            config.NewConfig<CreateGameSaveCommand, CreateGameSaveCommand>();
            config.NewConfig<UpdateGameSaveCommand, UpdateGameSaveCommand>();
        }
    }
}

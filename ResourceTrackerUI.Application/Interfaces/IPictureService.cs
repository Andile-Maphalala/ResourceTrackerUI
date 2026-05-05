
using Microsoft.AspNetCore.Components.Forms;
using ResourceTrackerUI.Domain.Enums;
using ResourceTrackerUI.Domain.Models.Feature.Picture;

namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IPictureService
    {
        Task<CreatePictureResponseModel> CreatePicture(PictureModel model, CancellationToken cancellationToken);
        Task DeletePicture(int pictureId, CancellationToken cancellationToken);
        Task<PictureResponseModel> GetPicture(int pictureId, CancellationToken cancellationToken);
        Task<List<PictureResponseModel>> GetPictureList(int? imageUploadType, CancellationToken cancellationToken);
        Task<CreatePictureResponseModel?> CrudPicture(FormModeEnum mode, bool imageChanged, int? imageId, IBrowserFile image, string name, int linkEntity, string? altText, int imageUploadType, CancellationToken cancellationToken);
    }
}

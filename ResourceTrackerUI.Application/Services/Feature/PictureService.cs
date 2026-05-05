

using MapsterMapper;
using Microsoft.AspNetCore.Components.Forms;
using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Enums;
using ResourceTrackerUI.Domain.Models.Feature.Picture;
using System.Reflection;
using ImageUploadTypeEnum = ResourceTrackerUI.ApiClient.ImageUploadTypeEnum;

namespace ResourceTrackerUI.Application.Services.Feature
{
    public class PictureService : IPictureService
    {
        private ResourceTrackerApiClient _apiClient { get; set; }
        private IMapper _mapper { get; set; }
        public PictureService(ResourceTrackerApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        public async Task<CreatePictureResponseModel> CreatePicture(PictureModel model, CancellationToken cancellationToken)
        {
            var fileparam = new FileParameter(model.Data.OpenReadStream(5242880), model.Data.Name, model.Data.ContentType);
            var response = await _apiClient.ApiPictureCreatePictureAsync(model.Name,model.AltText,fileparam, (ImageUploadTypeEnum?)model.ImageUploadType,model.LinkedEntityId, cancellationToken);
            var result = _mapper.Map<CreatePictureResponseModel>(response);
            return result;
        }

        public async Task DeletePicture(int pictureId, CancellationToken cancellationToken)
        {
            await _apiClient.ApiPictureDeletePictureAsync(pictureId, cancellationToken);
        }

        public async Task<PictureResponseModel> GetPicture(int pictureId, CancellationToken cancellationToken)
        {
           var response = await _apiClient.ApiPictureGetPictureAsync(pictureId, cancellationToken);
           var result = _mapper.Map<PictureResponseModel>(response);
           return result;
        }

        public async Task<List<PictureResponseModel>> GetPictureList(int? imageUploadType, CancellationToken cancellationToken)
        {
            var response = await _apiClient.ApiPictureGetPictureListAsync((ImageUploadTypeEnum?)imageUploadType, cancellationToken);
            var result = _mapper.Map<List<PictureResponseModel>>(response);
            return result;
        }

        public async Task<CreatePictureResponseModel?> CrudPicture(FormModeEnum mode , bool imageChanged, int? imageId, IBrowserFile image, string name, int linkEntity, string? altText, int imageUploadType,  CancellationToken cancellationToken)
        {
            var result = new CreatePictureResponseModel();
            if (imageChanged)
            {
                if (image == null && imageId != null)
                {
                    await DeletePicture(imageId.Value, cancellationToken);
                    return null;
                }
                else
                    if (image != null)
                    {
                        if (imageId != null)
                        {
                            await DeletePicture(imageId.Value, cancellationToken);
                        }
                        var picture = new PictureModel
                        {
                            Name = image.Name,
                            LinkedEntityId = linkEntity,
                            AltText = altText,
                            ImageUploadType = imageUploadType,
                            Data = image
                        };
                        await CreatePicture(picture, cancellationToken);
                        return null;
                  }
            }

            return result;
        }
    }
}

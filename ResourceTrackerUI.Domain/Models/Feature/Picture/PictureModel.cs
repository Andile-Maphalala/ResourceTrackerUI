

using Microsoft.AspNetCore.Components.Forms;
using ResourceTrackerUI.Domain.Enums;

namespace ResourceTrackerUI.Domain.Models.Feature.Picture
{
    public class PictureModel
    {
        public string Name { get; set; }
        public string? AltText { get; set; }
        public IBrowserFile Data { get; set; }
        public int ImageUploadType { get; set; }
        public int LinkedEntityId { get; set; }
    }
}

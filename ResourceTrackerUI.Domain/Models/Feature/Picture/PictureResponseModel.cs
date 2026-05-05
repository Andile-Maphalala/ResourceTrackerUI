
namespace ResourceTrackerUI.Domain.Models.Feature.Picture
{
    public class PictureResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AltText { get; set; }
        public long Size { get; set; }
        public string Url { get; set; }
        public int? ImageUploadType { get; set; }
        public string ImageUploadTypeName { get; set; }
    }
}

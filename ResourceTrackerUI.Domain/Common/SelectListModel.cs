

using System.Reflection.Metadata;

namespace ResourceTrackerUI.Domain.Common
{
    public class SelectListModel<T>
    {
        public T Value { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
    }
}

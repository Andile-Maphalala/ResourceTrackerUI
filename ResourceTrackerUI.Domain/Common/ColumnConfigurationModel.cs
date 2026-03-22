

namespace ResourceTrackerUI.Domain.Common
{
    public class ColumnConfigurationModel
    {
        public string PropertyName { get; set; } = string.Empty;
        public bool Visible { get; set; }
        public int Order { get; set; }
    }
}

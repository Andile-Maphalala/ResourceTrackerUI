using ResourceTrackerUI.Domain.Attributes;
using System.Reflection;

namespace ResourceTrackerUI.Domain.Common
{
    public class ColumnDefinition
    {
        public PropertyInfo PropertyInfo { get; }
        public string DisplayName { get; }
        public int Order { get; }
        public bool Visible { get; }
        public string? FormatString { get; }
        public bool Searchable { get; }
        public bool Sortable { get; set; } = true;
        public ColumnDefinition(PropertyInfo property)
        {
            PropertyInfo = property;

            var attr = property.GetCustomAttribute<CustomColumnAttribute>();

            DisplayName = attr?.DisplayName ?? property.Name;
            Order = attr?.Order ?? 0;
            Visible = attr?.Visible ?? true;
            FormatString = attr?.FormatString;
            Searchable = attr?.Searchable ?? true;
            Sortable = attr?.Sortable ?? true;
        }
    }
}

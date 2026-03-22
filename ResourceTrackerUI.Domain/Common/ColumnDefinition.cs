using ResourceTrackerUI.Domain.Attributes;
using System.Reflection;

namespace ResourceTrackerUI.Domain.Common
{
    public class ColumnDefinition
    {
        public PropertyInfo PropertyInfo { get;  set;}
        public string DisplayName { get;  set;}
        public int Order { get;  set;}
        public bool Visible { get;  set;}
        public string? FormatString { get;  set;}
        public bool Searchable { get;  set;}
        public bool Sortable { get; set; } = true;
        public bool HasAttribute { get;  set;}  // Add this property

        public ColumnDefinition(PropertyInfo property)
        {
            PropertyInfo = property;
            var attr = property.GetCustomAttribute<CustomColumnAttribute>();

            if(attr is null)
            {
                HasAttribute = false;
            }
            else
            {
                HasAttribute = true;
            }
            
            DisplayName = attr?.DisplayName ?? property.Name;
            Order = attr?.Order ?? 0;
            Visible = attr?.Visible ?? false;
            FormatString = attr?.FormatString;
            Searchable = attr?.Searchable ?? true;
            Sortable = attr?.Sortable ?? true;
        }
    }
}

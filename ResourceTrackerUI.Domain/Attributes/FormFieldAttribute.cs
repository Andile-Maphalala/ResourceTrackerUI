
namespace ResourceTrackerUI.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FormFieldAttribute : Attribute
    {
        public string DisplayName { get; set; } = "";
        public int MaxLength { get; set; }
        public string Placeholder { get; set; } = "";
        public string Format { get; set; } = "";

        public FormFieldAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public FormFieldAttribute()
        {
        }
    }
}

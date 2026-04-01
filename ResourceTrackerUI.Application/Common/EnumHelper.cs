using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Domain.Enums;
using System.ComponentModel;

namespace ResourceTrackerUI.Application.Common
{

    public static class EnumHelper
    {
        public static string GetEnumDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
                return value.ToString();

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            if (attribute == null)
                return value.ToString();

            var descriptionAttribute = attribute as DescriptionAttribute;
            if (descriptionAttribute == null)
                return value.ToString();

            return descriptionAttribute.Description;
        }

        public static ImageUploadTypeEnum? GetEnumValue(int? value)
        {
            if (value == null)
                return null;

            if (Enum.IsDefined(typeof(ImageUploadTypeEnum), value))
            {
                return (ImageUploadTypeEnum)value;
            }
            else
            {
                return null;
            }
        }

        public static bool IsReadOnlyValue(this FormModeEnum value)
        {
            switch (value)
            {
                case FormModeEnum.View:
                case FormModeEnum.Delete:
                    return true;
                default:
                    return false;
            }
        }
    }
        
}

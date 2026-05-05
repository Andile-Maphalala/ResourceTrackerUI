using ResourceTrackerUI.Domain.Common;
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

        public static IEnumerable<ValuePairModel<int>> GetEnumSelectList<TEnum>(bool includeAllOption = false, string allOptionText = "All") where TEnum : Enum
        {
            var data = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(x => new ValuePairModel<int>
                {
                    Id = Convert.ToInt32(x),
                    Name = x.GetEnumDescription()
                });

            if (includeAllOption)
            {
                var list = data.ToList();
                list.Insert(0, new ValuePairModel<int> { Id = -1, Name = allOptionText });
                return list;
            }

            return data;
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

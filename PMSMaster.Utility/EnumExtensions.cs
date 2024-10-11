
using System.ComponentModel;

namespace PMSMaster.Utility
{
    public static class EnumExtensions
    {
        public static string GetDisplayText(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));

            return attribute != null ? attribute.Description : value.ToString();
        }
    }
}

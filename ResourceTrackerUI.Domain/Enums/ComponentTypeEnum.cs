

using System.ComponentModel;

namespace ResourceTrackerUI.Domain.Enums
{
    public enum ComponentTypeEnum
    {
        [Description("Resource Component")]
        Resource = 1,

        [Description("Composite Component")]
        Composite = 2,

        [Description("Facility Component")]
        Facility = 3
    }
}

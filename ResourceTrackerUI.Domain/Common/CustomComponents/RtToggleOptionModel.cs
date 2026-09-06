

using Microsoft.AspNetCore.Components;

namespace ResourceTrackerUI.Domain.Common.CustomComponents
{
    public class RtToggleOptionModel
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public EventCallback OnSelected { get; set; }
    }
}

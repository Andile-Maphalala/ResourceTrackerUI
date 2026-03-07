using ResourceTrackerUI.Domain.Enums;

namespace ResourceTrackerUI.Domain.Common
{
    public class ActionModel<TItem>
    {
        public TItem Item { get; set; } = default!;
        public FormModeEnum Mode { get; set; }
    }
}

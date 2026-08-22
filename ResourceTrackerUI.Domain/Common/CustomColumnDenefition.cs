

using Microsoft.AspNetCore.Components;

namespace ResourceTrackerUI.Domain.Common
{
    public class CustomColumnDenefition<TItem>
    {
        public string DisplayName { get; set; } = string.Empty;

        public int Order { get; set; }

        public bool Visible { get; set; } = true;

        public bool Sortable { get; set; } = false;

        public Func<TItem, object>? SortBy { get; set; }

        public RenderFragment<TItem> Template { get; set; } = default!;

        public string? CellStyle { get; set; }
    }
}

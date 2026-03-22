using ResourceTrackerUI.Domain.Enums;

namespace ResourceTrackerUI.Domain.Models.Common
{
    public class PageableRequestModel
    {
        //
        // Summary:
        //     Gets or sets the page number.
        public int PageNumber { get; set; } = 1;

        //
        // Summary:
        //     Gets or sets the page size.
        public int PageSize { get; set; } = 10;

        //
        // Summary:
        //     Gets or sets the order by. Refers to the property name of the entity.
        public string OrderBy { get; set; } = string.Empty;

        //
        // Summary:
        //     Gets or sets the order direction. Default is ascending.
        public OrderDirectionEnum OrderDirection { get; set; } = OrderDirectionEnum.Ascending;

        public string? SearchTerms { get; set; } = string.Empty;
    }
}


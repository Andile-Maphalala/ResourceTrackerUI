using ResourceTrackerUI.Domain.Enums;

namespace ResourceTrackerUI.Domain.Models.Common
{
    public class PageableResponseModel<T>
    {
        //
        // Summary:
        //     Gets or sets the data. The collection of entities.
        public IEnumerable<T> Data { get; set; }

        //
        // Summary:
        //     Gets or sets the total records. The total number of records before pagination
        //     is applied.
        public int TotalRecords { get; set; }

        //
        // Summary:
        //     Gets or sets the page number. The Page Requested
        public int PageNumber { get; set; }

        //
        // Summary:
        //     Gets or sets the page size. The number of records per page.
        public int PageSize { get; set; }

        //
        // Summary:
        //     Gets or sets the page count. The total number of pages. Calculated from TotalRecords
        //     and PageSize.
        public int PageCount { get; set; }

        //
        // Summary:
        //     Gets or sets the order by. The property name of the entity.
        public string OrderBy { get; set; } = string.Empty;

        //
        // Summary:
        //     Gets or sets the order direction.
        public OrderDirectionEnum OrderDirection { get; set; }
    }
}

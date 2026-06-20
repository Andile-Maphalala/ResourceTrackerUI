

using ResourceTrackerUI.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ResourceTrackerUI.Domain.Models.Feature.Inventory
{
    public class InventoryCrudModel
    {
        public int Id { get; set; }

        [Required]
        [FormField(DisplayName = "Quantity")]
        public int AmountAquired { get; set; }
        public int ComponentId { get; set; }
    }
}

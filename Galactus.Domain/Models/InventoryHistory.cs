using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galactus.Domain.Models
{
    [Table(nameof(InventoryHistory), Schema = "Production")]
    public class InventoryHistory
    {
        [Key]
        public int InventoryHistoryId { get; set; }
        [ForeignKey(nameof(Models.Inventory))]
        public string InventoryId { get; set; }
        public short LocationId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(Models.Employee))]
        public int MovedHereByEmployeeId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Inventory Inventory { get; set; }
        [ForeignKey("ProductId, LocationId")]
        public virtual ProductInventory ProductInventory { get; set; }
        public virtual Employee MovedHereByEmployee { get; set; }
    }
}

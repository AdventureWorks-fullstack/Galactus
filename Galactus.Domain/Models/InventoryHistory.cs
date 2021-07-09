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
        [ForeignKey(nameof(Models.Location))]
        public short LocationId { get; set; }
        [ForeignKey(nameof(Models.Product))]
        public int ProductId { get; set; }
        [ForeignKey(nameof(Models.Employee))]
        public int BusinessEntityId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Inventory Inventory { get; set; }
        public virtual Product Product { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Location Location { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galactus.Domain.Models
{
    [Table(nameof(Inventory), Schema = "Production")]
    public class Inventory
    {
        public Inventory()
        {
            ProductInventory = new HashSet<ProductInventory>();
            InventoryHistory = new HashSet<InventoryHistory>();
        }

        [Key]
        public string InventoryId { get; set; }
        [ForeignKey(nameof(Models.Location))]
        public short LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<InventoryHistory> InventoryHistory { get; set; }
        public virtual ICollection<ProductInventory> ProductInventory { get; set; }
    }
}

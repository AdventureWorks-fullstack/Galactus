using System;
using System.Collections.Generic;

#nullable disable

namespace Galactus.Domain.Models
{
    // Product inventory information.
    public partial class ProductInventory
    {
        public ProductInventory()
        {
            InventoryHistory = new HashSet<InventoryHistory>();
        }

        public int ProductId { get; set; }
        public short LocationId { get; set; }
        public string InventoryId { get; set; }
        public short Quantity { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual ICollection<InventoryHistory> InventoryHistory { get; set; }
    }
}

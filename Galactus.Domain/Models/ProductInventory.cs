using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Galactus.Domain.Models
{
    // Product inventory information.ef
    public partial class ProductInventory
    {
        public ProductInventory()
        {
            StorageHistory = new HashSet<StorageHistory>();
        }

        // PK FK
        public int ProductId { get; set; }
        public short LocationId { get; set; }

        public short Quantity { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<Storage> Storage { get; set; }
        public virtual ICollection<StorageHistory> StorageHistory { get; set; }
    }
}

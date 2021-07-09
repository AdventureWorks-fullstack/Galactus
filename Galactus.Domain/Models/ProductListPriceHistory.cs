using System;
using System.Collections.Generic;

#nullable disable

namespace Galactus.Domain.Models
{
    // Changes in the list price of a product over time.
    public partial class ProductListPriceHistory
    {
        public int ProductId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal ListPrice { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Product Product { get; set; }
    }
}

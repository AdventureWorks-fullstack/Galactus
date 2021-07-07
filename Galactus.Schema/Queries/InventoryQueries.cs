using AdventureWorks.Domain;
using AdventureWorks.Domain.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galactus.Schema.Queries
{
    public partial class Query
    {
        [Serial]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Inventory> GetEmptyInventory([Service] AdventureWorksContext context, short locationId) =>
            context.Inventories.Where(x => x.LocationId == locationId && x.ProductInventory.Count() <= 0); 
    }
}
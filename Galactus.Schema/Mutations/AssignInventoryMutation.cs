using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventureWorks.Domain;
using AdventureWorks.Domain.Models;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace Galactus.Schema.Mutations
{
    public partial class Mutation
    {
        public async Task<AssignInventoryPayload> AssignInventoryMutation([Service] AdventureWorksContext context)
        {
            try
            {
                var result = new List<ProductInventory>();
                var productsWithoutInventory = await context.ProductInventories.Where(x => string.IsNullOrEmpty(x.InventoryId)).ToListAsync();

                if (productsWithoutInventory.Count() <= 0)
                    return new AssignInventoryPayload(result);

                var emptryInventories = await context.Inventories.Where(x => x.ProductInventory.Count() <= 0).ToListAsync();

                if (emptryInventories.Count() < emptryInventories.Count())
                    throw new Exception($"Not enough empty inventories! Need: {productsWithoutInventory.Count()} Have: {emptryInventories.Count()}");

                int i = 0;
                foreach (var product in productsWithoutInventory)
                {
                    product.Inventory = emptryInventories[i];
                    i++;
                }

                await context.SaveChangesAsync();
                return new AssignInventoryPayload(productsWithoutInventory);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public class AssignInventoryPayload
    {
        public AssignInventoryPayload(List<ProductInventory> productInventories)
        {
            ProductInventories = productInventories;
        }

        public List<ProductInventory> ProductInventories { get; set; }
    }
}
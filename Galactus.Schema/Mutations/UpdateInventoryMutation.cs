using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Domain;
using AdventureWorks.Domain.Models;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace Galactus.Schema.Mutations
{
    public partial class Mutation
    {
        public async Task<UpdateInventoryPayload> UpdateInventoryMutation(
            [Service] AdventureWorksContext context,
            UpdateInventoryInput input)
        {
            try
            {
                var productInventory = await context.ProductInventories
                    .FirstOrDefaultAsync(x => x.LocationId == input.LocationId && x.ProductId == input.ProductId);

                // A new product is being added
                if (productInventory is null)
                {
                    productInventory = new ProductInventory { ProductId = input.ProductId, LocationId = input.LocationId };
                    await context.ProductInventories.AddAsync(productInventory);
                }
                // Update the existing or new inventory
                if (input.UpdateAmount != 0)
                {
                    productInventory.Quantity += input.UpdateAmount;
                }
                if (!string.IsNullOrEmpty(input.NewLocationId) && productInventory.InventoryId != input.NewLocationId)
                {
                    productInventory.InventoryId = input.NewLocationId;

                    // TODO change enddate on the previous one
                    var inventoryHistory = new InventoryHistory
                    {
                        InventoryId = input.NewLocationId,
                        LocationId = input.LocationId,
                        ProductId = input.ProductId,
                        BusinessEntityId = input.EmployeeId,
                        StartDate = DateTime.Now
                    };

                    await context.InventoryHistories.AddAsync(inventoryHistory);
                }

                await context.SaveChangesAsync();
                return new UpdateInventoryPayload(productInventory);
            }
            catch
            {
                // TODO log ex
                throw;
            }
        }
    }

    public record UpdateInventoryInput(
        int ProductId,
        int EmployeeId,
        short LocationId,
        short UpdateAmount,
        string NewLocationId
    );

    public class UpdateInventoryPayload
    {
        public UpdateInventoryPayload(ProductInventory productInventory)
        {
            ProductInventory = productInventory;
        }

        public ProductInventory ProductInventory { get; set; }
    }
}
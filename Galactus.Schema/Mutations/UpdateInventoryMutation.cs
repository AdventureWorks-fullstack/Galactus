using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventureWorks.Domain;
using AdventureWorks.Domain.Models;
using Galactus.Schema.Common;
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
                    // Update the inventory position of the product
                    productInventory.InventoryId = input.NewLocationId;

                    var dateTime = DateTime.Now; 

                    // Create a mew history post
                    var inventoryHistory = new InventoryHistory
                    {
                        InventoryId = input.NewLocationId,
                        LocationId = input.LocationId,
                        ProductId = input.ProductId,
                        BusinessEntityId = input.EmployeeId,
                        StartDate = DateTime.Now
                    };

                    await context.InventoryHistories.AddAsync(inventoryHistory);

                    // Update EndDate of the previous history post

                    var previous = await context.InventoryHistories.FirstOrDefaultAsync(x =>
                    x.LocationId == input.LocationId &&
                    x.ProductId == input.ProductId &&
                    x.EndDate == null);
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

    public class UpdateInventoryPayload : Payload
    {
        public UpdateInventoryPayload(ProductInventory productInventory)
        {
            ProductInventory = productInventory;
        }
        public UpdateInventoryPayload(IReadOnlyList<UserError> errors) : base(errors) { }

        public ProductInventory? ProductInventory { get; set; }
    }
}
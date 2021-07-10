using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galactus.Domain;
using Galactus.Domain.Models;
using Galactus.Schema.Common;
using Galactus.Schema.Helpers;
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
                var inventory = await context.Inventories.FirstOrDefaultAsync(x => x.LocationId == input.LocationId && x.InventoryName == input.NewLocationName);

                if (inventory == null)
                    return new UpdateInventoryPayload(new List<UserError>() { new UserError($"Location does not exist '{input.NewLocationName}'", "400") });

                var productInventory = await DoWork(context, input, inventory);

                await context.SaveChangesAsync();

                return new UpdateInventoryPayload(productInventory);
            }
            catch
            {
                // TODO log ex
                throw;
            }
        }

        private async Task<ProductInventory> DoWork(AdventureWorksContext context, UpdateInventoryInput input, Inventory inventory)
        {
            var productInventory = await context.ProductInventories.Include(x => x.InventoryHistory).ThenInclude(x => x.Inventory)
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
            if (productInventory.InventoryId != inventory.InventoryId)
            {
                // Update the inventory position of the product
                productInventory.Inventory = inventory;

                await DbHelper.AddInventoryHistory(context, productInventory, input.EmployeeId);
            }

            return productInventory;
        }
    }

    public record UpdateInventoryInput(
        int ProductId,
        int EmployeeId,
        short LocationId,
        short UpdateAmount,
        string NewLocationName
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
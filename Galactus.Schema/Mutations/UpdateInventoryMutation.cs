using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galactus.Domain;
using Galactus.Domain.Models;
using Galactus.Schema.Common;
using Galactus.Schema.Helpers;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace Galactus.Schema.Mutations
{
    public partial class Mutation
    {
        [Serial]
        [UseProjection]
        public async Task<UpdateInventoryPayload> UpdateInventoryMutation(
            [Service] AdventureWorksContext context,
            UpdateInventoryInput input)
        {
            try
            {
                var result = new List<ProductInventory>();
                foreach (var update in input.Updates)
                {
                    await DoWork(context, input, update);
                }

                await context.SaveChangesAsync();
                return new UpdateInventoryPayload(result);
            }
            catch
            {
                // TODO log ex
                throw;
            }
        }

        private async Task<ProductInventory> DoWork(AdventureWorksContext context, UpdateInventoryInput input, InventoryUpdate update)
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
            if (productInventory.stor != inventory.InventoryId)
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
        InventoryUpdate[] Updates
    );

    public record InventoryUpdate(
        short UpdateAmount,
        string OldStorageName,
        string NewStorageName
        );

    public class UpdateInventoryPayload : Payload
    {
        public UpdateInventoryPayload(List<ProductInventory> productInventory)
        {
            ProductInventory = productInventory;
        }
        public UpdateInventoryPayload(IReadOnlyList<UserError> errors) : base(errors) { }

        public List<ProductInventory>? ProductInventory { get; set; }
    }
}
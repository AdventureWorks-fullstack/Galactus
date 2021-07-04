using System;
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
            UpdateInventoryInput input,
            [Service] AdventureWorksContext context
        )
        {
            string savePoint = nameof(UpdateInventoryMutation);
            using var transaction = context.Database.BeginTransaction();
            try
            {
                transaction.CreateSavepoint(savePoint);

                var productInventory = await context.ProductInventories
                .FirstOrDefaultAsync(x => x.LocationId == input.LocationId && x.ProductId == input.ProductId);

                if (input.UpdateAmount is not null && input.UpdateAmount != 0)
                {
                    productInventory.Quantity += (short)input.UpdateAmount;
                    await context.SaveChangesAsync();
                }
                if (!string.IsNullOrEmpty(input.NewShelf) && productInventory.Shelf != input.NewShelf)
                {
                    productInventory.Shelf = input.NewShelf;
                    await context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return new UpdateInventoryPayload(productInventory);
            }
            catch (Exception ex)
            {
                await transaction.RollbackToSavepointAsync(savePoint);
                throw;
            }
        }
    }

    public record UpdateInventoryInput(
        int ProductId,
        short LocationId,
        short? UpdateAmount,
        string NewShelf
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
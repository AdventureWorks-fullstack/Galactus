//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Galactus.Domain;
//using Galactus.Domain.Models;
//using Galactus.Schema.Common;
//using Galactus.Schema.Helpers;
//using HotChocolate;
//using Microsoft.EntityFrameworkCore;

//namespace Galactus.Schema.Mutations
//{
//    public partial class Mutation
//    {
//        public async Task<AssignInventoryPayload> AssignInventoryMutation([Service] AdventureWorksContext context, AssignInventoryInput input)
//        {
//            try
//            {
//                var result = new List<ProductInventory>();
//                var productsWithoutInventory = await context.ProductInventories
//                    .Where(x => x.LocationId == input.LocationId && x.InventoryId == null).ToListAsync();

//                if (productsWithoutInventory.Count() <= 0)
//                    return new AssignInventoryPayload(result);

//                var emptryInventories = await context.Inventories
//                    .Where(x => x.LocationId == input.LocationId && x.ProductInventory.Count() <= 0).ToListAsync();

//                if (emptryInventories.Count() < emptryInventories.Count())
//                    throw new Exception($"Not enough empty inventories! Need: {productsWithoutInventory.Count()} Have: {emptryInventories.Count()}");

//                for (int i = 0; i < productsWithoutInventory.Count; i++)
//                {
//                    var product = productsWithoutInventory[i];
//                    var inventory = emptryInventories[i];

//                    product.Inventory = inventory;

//                    await DbHelper.AddInventoryHistory(context, product, null);
//                }

//                await context.SaveChangesAsync();
//                return new AssignInventoryPayload(productsWithoutInventory);
//            }
//            catch
//            {
//                // TODO log ex
//                throw;
//            }
//        }
//    }

//    public record AssignInventoryInput(
//        int LocationId
//        );

//    public class AssignInventoryPayload : Payload
//    {
//        public AssignInventoryPayload(List<ProductInventory> productInventories)
//        {
//            ProductInventories = productInventories;
//        }
//        public AssignInventoryPayload(IReadOnlyList<UserError> errors) : base(errors) {}

//        public List<ProductInventory>? ProductInventories { get; set; }
//    }
//}
using Galactus.Domain;
using Galactus.Domain.Models;
using Galactus.Schema.Mutations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactus.Schema.Helpers
{
    public static class DbHelper
    {
        public static async Task<StorageHistory> AddInventoryHistory(
            AdventureWorksContext context,
            string newLocationName,
            short locationId,
            int productId,
            int? employeeId)
        {
            var dateTime = DateTime.Now;

            // Create a mew history post
            var inventoryHistory = new StorageHistory
            {
                StorageId = productInventory.Storage.i,
                LocationId = productInventory.LocationId,
                ProductId = productInventory.ProductId,
                MovedHereByEmployeeId = employeeId,
                StartDate = dateTime
            };

            await context.InventoryHistories.AddAsync(inventoryHistory);

            // Update previous 
            var previous = await context.InventoryHistories
                .Where(x => x.LocationId == productInventory.LocationId && x.ProductId == productInventory.ProductId && x.EndDate == null)
                .OrderBy(x => x.EndDate)
                .FirstOrDefaultAsync();

            if (previous != null)
                previous.EndDate = dateTime;

            return inventoryHistory;
        }
    }
}

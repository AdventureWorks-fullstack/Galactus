using AdventureWorks.Domain;
using AdventureWorks.Domain.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galactus.Schema
{
    public class Query
    {
        [Serial]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Customer> GetCustomers([Service] AdventureWorksContext context) =>
            context.Customers;

        [Serial]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Department> GetDepartments([Service] AdventureWorksContext context) =>
            context.Departments;

        [Serial]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Employee> GetEmployees([Service] AdventureWorksContext context) =>
            context.Employees;

        [Serial]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<AdventureWorks.Domain.Models.Location> GetLocations([Service] AdventureWorksContext context) =>
            context.Locations;

        [Serial]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Product> GetProducts([Service] AdventureWorksContext context) =>
            context.Products;

        [Serial]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<PurchaseOrderHeader> GetPurchases([Service] AdventureWorksContext context) =>
            context.PurchaseOrderHeaders;

        [Serial]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<SalesOrderHeader> GetSales([Service] AdventureWorksContext context) =>
            context.SalesOrderHeaders;

        [Serial]
        [UseProjection]
        public IQueryable<ShoppingCartItem> GetShoppingCartById([Service] AdventureWorksContext context, string shoppingCartId) =>
            context.ShoppingCartItems.Where(x => x.ShoppingCartId == shoppingCartId);

        // TODO temp crap
        [Serial]
        public Task<List<string>> GetShoppingCarts([Service] AdventureWorksContext context) =>
            context.ShoppingCartItems.Select(x => x.ShoppingCartId).ToListAsync();

        [Serial]
        [UseProjection]
        public IQueryable<SpecialOffer> GetSpecials([Service] AdventureWorksContext context) =>
            context.SpecialOffers;

        [Serial]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Store> GetStores([Service] AdventureWorksContext context) =>
            context.Stores;

        [Serial]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Vendor> GetVendors([Service] AdventureWorksContext context) =>
            context.Vendors;

        [Serial]
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<WorkOrder> GetWorkOrders([Service] AdventureWorksContext context) =>
            context.WorkOrders;
    }
}

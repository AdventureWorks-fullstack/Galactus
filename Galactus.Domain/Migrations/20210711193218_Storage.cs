using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Galactus.Domain.Migrations
{
    public partial class Storage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_ProductInventory_Bin",
                table: "ProductInventory",
                schema: "Production");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ProductInventory_Shelf",
                table: "ProductInventory",
                schema: "Production");

            migrationBuilder.DropColumn(
                name: "Bin",
                schema: "Production",
                table: "ProductInventory");

            migrationBuilder.DropColumn(
                name: "Shelf",
                schema: "Production",
                table: "ProductInventory");

            migrationBuilder.CreateTable(
                name: "Shelf",
                schema: "Production",
                columns: table => new
                {
                    ShelfId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelf", x => x.ShelfId);
                });

            migrationBuilder.CreateTable(
                name: "Storage",
                schema: "Production",
                columns: table => new
                {
                    StorageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StorageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorageType = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<short>(type: "smallint", nullable: false),
                    ShelfId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storage", x => x.StorageId);
                    table.ForeignKey(
                        name: "FK_Storage_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Production",
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Storage_Shelf_ShelfId",
                        column: x => x.ShelfId,
                        principalSchema: "Production",
                        principalTable: "Shelf",
                        principalColumn: "ShelfId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductInventoryStorage",
                schema: "Production",
                columns: table => new
                {
                    StorageId = table.Column<int>(type: "int", nullable: false),
                    ProductInventoryProductId = table.Column<int>(type: "int", nullable: false),
                    ProductInventoryLocationId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventoryStorage", x => new { x.StorageId, x.ProductInventoryProductId, x.ProductInventoryLocationId });
                    table.ForeignKey(
                        name: "FK_ProductInventoryStorage_ProductInventory_ProductInventoryProductId_ProductInventoryLocationId",
                        columns: x => new { x.ProductInventoryProductId, x.ProductInventoryLocationId },
                        principalSchema: "Production",
                        principalTable: "ProductInventory",
                        principalColumns: new[] { "ProductID", "LocationID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInventoryStorage_Storage_StorageId",
                        column: x => x.StorageId,
                        principalSchema: "Production",
                        principalTable: "Storage",
                        principalColumn: "StorageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StorageHistory",
                schema: "Production",
                columns: table => new
                {
                    StorageHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StorageId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<short>(type: "smallint", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MovedHereByEmployeeId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageHistory", x => x.StorageHistoryId);
                    table.ForeignKey(
                        name: "FK_StorageHistory_Employee_MovedHereByEmployeeId",
                        column: x => x.MovedHereByEmployeeId,
                        principalSchema: "HumanResources",
                        principalTable: "Employee",
                        principalColumn: "BusinessEntityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StorageHistory_ProductInventory_ProductId_LocationId",
                        columns: x => new { x.ProductId, x.LocationId },
                        principalSchema: "Production",
                        principalTable: "ProductInventory",
                        principalColumns: new[] { "ProductID", "LocationID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorageHistory_Storage_StorageId",
                        column: x => x.StorageId,
                        principalSchema: "Production",
                        principalTable: "Storage",
                        principalColumn: "StorageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventoryStorage_ProductInventoryProductId_ProductInventoryLocationId",
                schema: "Production",
                table: "ProductInventoryStorage",
                columns: new[] { "ProductInventoryProductId", "ProductInventoryLocationId" });

            migrationBuilder.CreateIndex(
                name: "IX_Storage_LocationId",
                schema: "Production",
                table: "Storage",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Storage_ShelfId",
                schema: "Production",
                table: "Storage",
                column: "ShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageHistory_MovedHereByEmployeeId",
                schema: "Production",
                table: "StorageHistory",
                column: "MovedHereByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageHistory_ProductId_LocationId",
                schema: "Production",
                table: "StorageHistory",
                columns: new[] { "ProductId", "LocationId" });

            migrationBuilder.CreateIndex(
                name: "IX_StorageHistory_StorageId",
                schema: "Production",
                table: "StorageHistory",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDetail_Product_ProductID",
                schema: "Sales",
                table: "SalesOrderDetail",
                column: "ProductID",
                principalSchema: "Production",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDetail_Product_ProductID",
                schema: "Sales",
                table: "SalesOrderDetail");

            migrationBuilder.DropTable(
                name: "ProductInventoryStorage",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "StorageHistory",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Storage",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Shelf",
                schema: "Production");

            migrationBuilder.AddColumn<byte>(
                name: "Bin",
                schema: "Production",
                table: "ProductInventory",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "Storage container on a shelf in an inventory location.");

            migrationBuilder.AddColumn<string>(
                name: "Shelf",
                schema: "Production",
                table: "ProductInventory",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                comment: "Storage compartment within an inventory location.");
        }
    }
}

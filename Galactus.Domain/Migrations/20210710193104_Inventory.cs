using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Galactus.Domain.Migrations
{
    public partial class Inventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_ProductInventory_Shelf",
                table: "ProductInventory",
                schema: "Production");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ProductInventory_Bin",
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

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                schema: "Production",
                table: "ProductInventory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Inventory",
                schema: "Production",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_Inventory_Location_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Production",
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryHistory",
                schema: "Production",
                columns: table => new
                {
                    InventoryHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<short>(type: "smallint", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MovedHereByEmployeeId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryHistory", x => x.InventoryHistoryId);
                    table.ForeignKey(
                        name: "FK_InventoryHistory_Employee_MovedHereByEmployeeId",
                        column: x => x.MovedHereByEmployeeId,
                        principalSchema: "HumanResources",
                        principalTable: "Employee",
                        principalColumn: "BusinessEntityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryHistory_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalSchema: "Production",
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryHistory_ProductInventory_ProductId_LocationId",
                        columns: x => new { x.ProductId, x.LocationId },
                        principalSchema: "Production",
                        principalTable: "ProductInventory",
                        principalColumns: new[] { "ProductID", "LocationID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventory_InventoryId",
                schema: "Production",
                table: "ProductInventory",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_LocationId",
                schema: "Production",
                table: "Inventory",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHistory_InventoryId",
                schema: "Production",
                table: "InventoryHistory",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHistory_MovedHereByEmployeeId",
                schema: "Production",
                table: "InventoryHistory",
                column: "MovedHereByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHistory_ProductId_LocationId",
                schema: "Production",
                table: "InventoryHistory",
                columns: new[] { "ProductId", "LocationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventory_Inventory_InventoryId",
                schema: "Production",
                table: "ProductInventory",
                column: "InventoryId",
                principalSchema: "Production",
                principalTable: "Inventory",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_ProductInventory_Inventory_InventoryId",
                schema: "Production",
                table: "ProductInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDetail_Product_ProductID",
                schema: "Sales",
                table: "SalesOrderDetail");

            migrationBuilder.DropTable(
                name: "InventoryHistory",
                schema: "Production");

            migrationBuilder.DropTable(
                name: "Inventory",
                schema: "Production");

            migrationBuilder.DropIndex(
                name: "IX_ProductInventory_InventoryId",
                schema: "Production",
                table: "ProductInventory");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                schema: "Production",
                table: "ProductInventory");

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

using Microsoft.EntityFrameworkCore.Migrations;

namespace Galactus.Domain.Migrations
{
    public partial class RemoveLink_InventoryHistoryProductInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryHistory_Employee_BusinessEntityId",
                schema: "Production",
                table: "InventoryHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryHistory_Location_LocationId",
                schema: "Production",
                table: "InventoryHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryHistory_Product_ProductId",
                schema: "Production",
                table: "InventoryHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryHistory_ProductInventory_ProductInventoryProductId_ProductInventoryLocationId",
                schema: "Production",
                table: "InventoryHistory");

            migrationBuilder.DropIndex(
                name: "IX_InventoryHistory_LocationId",
                schema: "Production",
                table: "InventoryHistory");

            migrationBuilder.DropIndex(
                name: "IX_InventoryHistory_ProductId",
                schema: "Production",
                table: "InventoryHistory");

            migrationBuilder.DropIndex(
                name: "IX_InventoryHistory_ProductInventoryProductId_ProductInventoryLocationId",
                schema: "Production",
                table: "InventoryHistory");

            migrationBuilder.DropColumn(
                name: "ProductInventoryLocationId",
                schema: "Production",
                table: "InventoryHistory");

            migrationBuilder.DropColumn(
                name: "ProductInventoryProductId",
                schema: "Production",
                table: "InventoryHistory");

            migrationBuilder.RenameColumn(
                name: "BusinessEntityId",
                schema: "Production",
                table: "InventoryHistory",
                newName: "MovedHereByEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryHistory_BusinessEntityId",
                schema: "Production",
                table: "InventoryHistory",
                newName: "IX_InventoryHistory_MovedHereByEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryHistory_Employee_MovedHereByEmployeeId",
                schema: "Production",
                table: "InventoryHistory",
                column: "MovedHereByEmployeeId",
                principalSchema: "HumanResources",
                principalTable: "Employee",
                principalColumn: "BusinessEntityID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryHistory_Employee_MovedHereByEmployeeId",
                schema: "Production",
                table: "InventoryHistory");

            migrationBuilder.RenameColumn(
                name: "MovedHereByEmployeeId",
                schema: "Production",
                table: "InventoryHistory",
                newName: "BusinessEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryHistory_MovedHereByEmployeeId",
                schema: "Production",
                table: "InventoryHistory",
                newName: "IX_InventoryHistory_BusinessEntityId");

            migrationBuilder.AddColumn<short>(
                name: "ProductInventoryLocationId",
                schema: "Production",
                table: "InventoryHistory",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductInventoryProductId",
                schema: "Production",
                table: "InventoryHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHistory_LocationId",
                schema: "Production",
                table: "InventoryHistory",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHistory_ProductId",
                schema: "Production",
                table: "InventoryHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHistory_ProductInventoryProductId_ProductInventoryLocationId",
                schema: "Production",
                table: "InventoryHistory",
                columns: new[] { "ProductInventoryProductId", "ProductInventoryLocationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryHistory_Employee_BusinessEntityId",
                schema: "Production",
                table: "InventoryHistory",
                column: "BusinessEntityId",
                principalSchema: "HumanResources",
                principalTable: "Employee",
                principalColumn: "BusinessEntityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryHistory_Location_LocationId",
                schema: "Production",
                table: "InventoryHistory",
                column: "LocationId",
                principalSchema: "Production",
                principalTable: "Location",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryHistory_Product_ProductId",
                schema: "Production",
                table: "InventoryHistory",
                column: "ProductId",
                principalSchema: "Production",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryHistory_ProductInventory_ProductInventoryProductId_ProductInventoryLocationId",
                schema: "Production",
                table: "InventoryHistory",
                columns: new[] { "ProductInventoryProductId", "ProductInventoryLocationId" },
                principalSchema: "Production",
                principalTable: "ProductInventory",
                principalColumns: new[] { "ProductID", "LocationID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}

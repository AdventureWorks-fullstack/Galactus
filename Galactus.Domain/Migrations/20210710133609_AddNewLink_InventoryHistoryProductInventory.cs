using Microsoft.EntityFrameworkCore.Migrations;

namespace Galactus.Domain.Migrations
{
    public partial class AddNewLink_InventoryHistoryProductInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InventoryHistory_ProductId_LocationId",
                schema: "Production",
                table: "InventoryHistory",
                columns: new[] { "ProductId", "LocationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryHistory_ProductInventory_ProductId_LocationId",
                schema: "Production",
                table: "InventoryHistory",
                columns: new[] { "ProductId", "LocationId" },
                principalSchema: "Production",
                principalTable: "ProductInventory",
                principalColumns: new[] { "ProductID", "LocationID" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryHistory_ProductInventory_ProductId_LocationId",
                schema: "Production",
                table: "InventoryHistory");

            migrationBuilder.DropIndex(
                name: "IX_InventoryHistory_ProductId_LocationId",
                schema: "Production",
                table: "InventoryHistory");
        }
    }
}

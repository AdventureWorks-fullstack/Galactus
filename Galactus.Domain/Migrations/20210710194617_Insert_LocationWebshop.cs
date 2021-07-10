using Microsoft.EntityFrameworkCore.Migrations;

namespace Galactus.Domain.Migrations
{
    public partial class Insert_LocationWebshop : Migration
    {
        string locationName = "Webshop Warehouse";
        static int locationId = 61;

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                $"INSERT INTO Production.Location (Name) VALUES ('{locationName}')"
            );

            migrationBuilder.Sql(
                $@"DECLARE @MyCursor CURSOR;
                DECLARE @MyField int;
                DECLARE @LocationId smallint;

                BEGIN
                SELECT @LocationId = LocationId
                    FROM Production.Location
                    WHERE Name = '{locationName}'

                SET @MyCursor = CURSOR FOR
                    SELECT ProductID FROM Production.Product

                OPEN @MyCursor
                    FETCH NEXT FROM @MyCursor
                    INTO @MyField

                WHILE @@FETCH_STATUS = 0
                BEGIN
                    INSERT INTO Production.ProductInventory(ProductID, LocationID, Quantity) VALUES (@MyField, @LocationId, 1)
                    FETCH NEXT FROM @MyCursor
                    INTO @MyField
                END;

                CLOSE @MyCursor;
                DEALLOCATE @MyCursor;
                END;"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                $"DELETE FROM Production.ProductInventory WHERE LocationId = {locationId}"
            );

            migrationBuilder.Sql(
                $"DELETE FROM Production.Location WHERE LocationId = {locationId}"
            );
        }
    }
}

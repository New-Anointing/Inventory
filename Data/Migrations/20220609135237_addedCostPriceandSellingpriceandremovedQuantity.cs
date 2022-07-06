using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_keeping.Data.Migrations
{
    public partial class addedCostPriceandSellingpriceandremovedQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Product",
                newName: "SellingPrice");

            migrationBuilder.AddColumn<double>(
                name: "CostPrice",
                table: "Product",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostPrice",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "SellingPrice",
                table: "Product",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

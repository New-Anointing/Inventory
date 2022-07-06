using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_keeping.Data.Migrations
{
    public partial class updatedPurchaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Purchase",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiscountPrice",
                table: "Purchase",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Purchase",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TaxPrice",
                table: "Purchase",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalAmount",
                table: "Purchase",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "TaxPrice",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Purchase");
        }
    }
}

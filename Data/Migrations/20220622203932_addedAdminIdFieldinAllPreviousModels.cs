using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_keeping.Data.Migrations
{
    public partial class addedAdminIdFieldinAllPreviousModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Branch",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Branch");
        }
    }
}

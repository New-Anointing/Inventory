using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_keeping.Data.Migrations
{
    public partial class removeUserAdminIdandAdminForiegnKeyFromModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAdminId",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "UserAdminId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UserAdminId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UserAdminId",
                table: "Branch");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAdminId",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserAdminId",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserAdminId",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserAdminId",
                table: "Branch",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_keeping.Data.Migrations
{
    public partial class addAdminIdFromApplicationUserAsForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Supplier",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserAdminId",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Product",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserAdminId",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Category",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserAdminId",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Branch",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserAdminId",
                table: "Branch",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_AdminId",
                table: "Supplier",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_AdminId",
                table: "Product",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_AdminId",
                table: "Category",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_AdminId",
                table: "Branch",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_AspNetUsers_AdminId",
                table: "Branch",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_AdminId",
                table: "Category",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AspNetUsers_AdminId",
                table: "Product",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_AspNetUsers_AdminId",
                table: "Supplier",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_AspNetUsers_AdminId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_AdminId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_AspNetUsers_AdminId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_AspNetUsers_AdminId",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Supplier_AdminId",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Product_AdminId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Category_AdminId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Branch_AdminId",
                table: "Branch");

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

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Branch",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

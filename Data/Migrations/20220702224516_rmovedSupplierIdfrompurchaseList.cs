using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_keeping.Data.Migrations
{
    public partial class rmovedSupplierIdfrompurchaseList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseList_Supplier_SupplierId",
                table: "PurchaseList");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseList_SupplierId",
                table: "PurchaseList");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "PurchaseList");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "PurchaseList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseList_SupplierId",
                table: "PurchaseList",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseList_Supplier_SupplierId",
                table: "PurchaseList",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

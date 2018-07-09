using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InventoryItemColumnModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_VoucherDetail_Voucher",
                table: "InventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItems_Voucher",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "Voucher",
                table: "InventoryItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Voucher",
                table: "InventoryItems",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_Voucher",
                table: "InventoryItems",
                column: "Voucher");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_VoucherDetail_Voucher",
                table: "InventoryItems",
                column: "Voucher",
                principalTable: "VoucherDetail",
                principalColumn: "VoucherNo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

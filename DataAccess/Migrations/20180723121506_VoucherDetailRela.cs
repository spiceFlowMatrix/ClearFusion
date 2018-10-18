using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class VoucherDetailRela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropForeignKey(
				name: "FK_VoucherTransactionDetails_VoucherDetail_VoucherNo",
				table: "VoucherTransactionDetails");

			migrationBuilder.DropIndex(
				name: "IX_VoucherTransactionDetails_VoucherNo",
				table: "VoucherTransactionDetails");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.CreateIndex(
				name: "IX_VoucherTransactionDetails_VoucherNo",
				table: "VoucherTransactionDetails",
				column: "VoucherId");

			migrationBuilder.AddForeignKey(
				name: "FK_VoucherTransactionDetails_VoucherDetail_VoucherNo",
				table: "VoucherTransactionDetails",
				column: "VoucherNo",
				principalTable: "VoucherDetail",
				principalColumn: "VoucherNo",
				onDelete: ReferentialAction.Restrict);
		}
    }
}

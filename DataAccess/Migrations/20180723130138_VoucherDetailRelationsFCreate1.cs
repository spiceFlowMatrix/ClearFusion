using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class VoucherDetailRelationsFCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VoucherDocumentDetail_VoucherNo",
                table: "VoucherDocumentDetail",
                column: "VoucherNo");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_VoucherId",
                table: "StoreItemPurchases",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItemPurchases_VoucherDetail_VoucherId",
                table: "StoreItemPurchases",
                column: "VoucherId",
                principalTable: "VoucherDetail",
                principalColumn: "VoucherNo",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDocumentDetail_VoucherDetail_VoucherNo",
                table: "VoucherDocumentDetail",
                column: "VoucherNo",
                principalTable: "VoucherDetail",
                principalColumn: "VoucherNo",
                onDelete: ReferentialAction.Cascade);

			migrationBuilder.CreateIndex(
				name: "IX_VoucherTransactionDetails_VoucherNo",
				table: "VoucherTransactionDetails",
				column: "VoucherNo");

			migrationBuilder.AddForeignKey(
				name: "FK_VoucherTransactionDetails_VoucherDetail_VoucherNo",
				table: "VoucherTransactionDetails",
				column: "VoucherNo",
				principalTable: "VoucherDetail",
				principalColumn: "VoucherNo",
				onDelete: ReferentialAction.Restrict);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreItemPurchases_VoucherDetail_VoucherId",
                table: "StoreItemPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDocumentDetail_VoucherDetail_VoucherNo",
                table: "VoucherDocumentDetail");

            migrationBuilder.DropIndex(
                name: "IX_VoucherDocumentDetail_VoucherNo",
                table: "VoucherDocumentDetail");

            migrationBuilder.DropIndex(
                name: "IX_StoreItemPurchases_VoucherId",
                table: "StoreItemPurchases");

			migrationBuilder.DropForeignKey(
				name: "FK_VoucherTransactionDetails_VoucherDetail_VoucherNo",
				table: "VoucherTransactionDetails");

			migrationBuilder.DropIndex(
				name: "IX_VoucherTransactionDetails_VoucherNo",
				table: "VoucherTransactionDetails");
		}
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class IndexVoucherTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_TransactionId",
                table: "VoucherTransactions",
                column: "TransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_TransactionDate_AccountNo",
                table: "VoucherTransactions",
                columns: new[] { "TransactionDate", "AccountNo" });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_VoucherNo",
                table: "VoucherDetail",
                column: "VoucherNo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_TransactionId",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_TransactionDate_AccountNo",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherDetail_VoucherNo",
                table: "VoucherDetail");
        }
    }
}

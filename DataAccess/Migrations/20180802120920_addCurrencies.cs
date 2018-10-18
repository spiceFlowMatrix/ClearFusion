using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addCurrencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AFGAmount",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "EURAmount",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PKRAmount",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "USDAmount",
                table: "VoucherTransactionDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AFGAmount",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "EURAmount",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "PKRAmount",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "USDAmount",
                table: "VoucherTransactionDetails");
        }
    }
}

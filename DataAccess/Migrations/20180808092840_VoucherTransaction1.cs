using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class VoucherTransaction1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Debit",
                table: "VoucherTransactions",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Credit",
                table: "VoucherTransactions",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Debit",
                table: "VoucherTransactions",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Credit",
                table: "VoucherTransactions",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}

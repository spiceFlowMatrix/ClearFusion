using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class intToLongVoucherTransactionTbleChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BudgetLineId",
                table: "VoucherTransactions",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BudgetLineId",
                table: "VoucherTransactions",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}

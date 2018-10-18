using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class PayrollAccountHeadTableAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TransactionTypeId",
                table: "SalaryHeadDetails",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "AccountNo",
                table: "SalaryHeadDetails",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TransactionTypeId",
                table: "SalaryHeadDetails",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AccountNo",
                table: "SalaryHeadDetails",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}

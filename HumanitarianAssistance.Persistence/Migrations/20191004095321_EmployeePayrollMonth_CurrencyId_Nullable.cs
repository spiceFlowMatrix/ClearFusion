using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class EmployeePayrollMonth_CurrencyId_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayrollMonth_CurrencyDetails_CurrencyId",
                table: "EmployeePayrollMonth");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "EmployeePayrollMonth",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayrollMonth_CurrencyDetails_CurrencyId",
                table: "EmployeePayrollMonth",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayrollMonth_CurrencyDetails_CurrencyId",
                table: "EmployeePayrollMonth");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "EmployeePayrollMonth",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayrollMonth_CurrencyDetails_CurrencyId",
                table: "EmployeePayrollMonth",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

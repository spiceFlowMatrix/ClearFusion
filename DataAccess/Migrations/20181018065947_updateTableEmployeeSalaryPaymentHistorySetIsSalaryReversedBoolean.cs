using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateTableEmployeeSalaryPaymentHistorySetIsSalaryReversedBoolean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSalaryReversed",
                table: "EmployeeSalaryPaymentHistory");

            migrationBuilder.AddColumn<bool>(
                name: "IsSalaryReverse",
                table: "EmployeeSalaryPaymentHistory",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSalaryReverse",
                table: "EmployeeSalaryPaymentHistory");

            migrationBuilder.AddColumn<int>(
                name: "IsSalaryReversed",
                table: "EmployeeSalaryPaymentHistory",
                nullable: false,
                defaultValue: 0);
        }
    }
}

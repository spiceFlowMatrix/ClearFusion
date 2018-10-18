using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeePayrollMonthColumnAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountNo",
                table: "EmployeePayrollMonth",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId",
                table: "EmployeePayrollMonth",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "EmployeePayrollMonth");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "EmployeePayrollMonth");
        }
    }
}

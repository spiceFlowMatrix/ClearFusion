using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeePayrollColumnAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AdvanceDeductibles",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SalaryTaxDeductibles",
                table: "EmployeePayroll",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvanceDeductibles",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "SalaryTaxDeductibles",
                table: "EmployeePayroll");
        }
    }
}

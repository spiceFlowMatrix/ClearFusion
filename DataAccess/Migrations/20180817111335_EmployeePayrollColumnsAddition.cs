using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeePayrollColumnsAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdvanceDeductibles",
                table: "EmployeePayroll",
                newName: "HourlyRate");

            migrationBuilder.AddColumn<float>(
                name: "AdvancesDeductibles",
                table: "EmployeePayroll",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvancesDeductibles",
                table: "EmployeePayroll");

            migrationBuilder.RenameColumn(
                name: "HourlyRate",
                table: "EmployeePayroll",
                newName: "AdvanceDeductibles");
        }
    }
}

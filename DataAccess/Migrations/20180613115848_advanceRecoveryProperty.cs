using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class advanceRecoveryProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AdvanceRecoveryAmount",
                table: "EmployeePayrollForMonth",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdvanceRecovery",
                table: "EmployeePayrollForMonth",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "AdvanceRecoveryAmount",
                table: "EmployeePaymentTypes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdvanceRecovery",
                table: "EmployeePaymentTypes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvanceRecoveryAmount",
                table: "EmployeePayrollForMonth");

            migrationBuilder.DropColumn(
                name: "IsAdvanceRecovery",
                table: "EmployeePayrollForMonth");

            migrationBuilder.DropColumn(
                name: "AdvanceRecoveryAmount",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropColumn(
                name: "IsAdvanceRecovery",
                table: "EmployeePaymentTypes");
        }
    }
}

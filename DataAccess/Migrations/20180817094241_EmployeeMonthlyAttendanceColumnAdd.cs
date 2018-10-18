using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeMonthlyAttendanceColumnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Casual",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "Emergency",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "RegCode",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.RenameColumn(
                name: "Medical",
                table: "EmployeeMonthlyAttendance",
                newName: "PaymentType");

            migrationBuilder.RenameColumn(
                name: "Maternity",
                table: "EmployeeMonthlyAttendance",
                newName: "CurrencyId");

            migrationBuilder.AddColumn<double>(
                name: "AdvanceAmount",
                table: "EmployeeMonthlyAttendance",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AdvanceRecoveryAmount",
                table: "EmployeeMonthlyAttendance",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GrossSalary",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HourlyRate",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdvanceApproved",
                table: "EmployeeMonthlyAttendance",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdvanceRecovery",
                table: "EmployeeMonthlyAttendance",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "NetSalary",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PensionAmount",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PensionRate",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SalaryTax",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalAllowance",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalDeduction",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalGeneralAmount",
                table: "EmployeeMonthlyAttendance",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvanceAmount",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "AdvanceRecoveryAmount",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "GrossSalary",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "IsAdvanceApproved",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "IsAdvanceRecovery",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "NetSalary",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "PensionAmount",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "PensionRate",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "SalaryTax",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "TotalAllowance",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "TotalDeduction",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "TotalGeneralAmount",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.RenameColumn(
                name: "PaymentType",
                table: "EmployeeMonthlyAttendance",
                newName: "Medical");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "EmployeeMonthlyAttendance",
                newName: "Maternity");

            migrationBuilder.AddColumn<int>(
                name: "Casual",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Emergency",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegCode",
                table: "EmployeeMonthlyAttendance",
                nullable: true);
        }
    }
}

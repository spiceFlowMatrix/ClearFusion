using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeePayrollChanges1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvancesDeductibles",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "CapacityBuildingDeductibles",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "CasualLeaveAllowance",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "EmergencyLeaveAllowance",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "FinesDeductibles",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "FoodAllowance",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "MaternityLeaveAllowance",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "MedicalAllowance",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "MedicalLeaveAllowance",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "OtherAllowance",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "OtherDeductibles",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "PensionDeductibles",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "SalaryTaxDeductibles",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "SecurityDeductibles",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "TrAllowance",
                table: "EmployeePayroll");

            migrationBuilder.AlterColumn<double>(
                name: "BasicPay",
                table: "EmployeePayroll",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "BasicPay",
                table: "EmployeePayroll",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AdvancesDeductibles",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CapacityBuildingDeductibles",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CasualLeaveAllowance",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "EmergencyLeaveAllowance",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FinesDeductibles",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FoodAllowance",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "HourlyRate",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MaternityLeaveAllowance",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MedicalAllowance",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MedicalLeaveAllowance",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "OtherAllowance",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "OtherDeductibles",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PensionDeductibles",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SalaryTaxDeductibles",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SecurityDeductibles",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TrAllowance",
                table: "EmployeePayroll",
                nullable: true);
        }
    }
}

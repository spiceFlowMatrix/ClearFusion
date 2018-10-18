using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DataTransferEmployeePayroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayroll_CurrencyDetails_CurrencyId",
                table: "EmployeePayroll");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayroll_SalaryHeadDetails_SalaryHeadId",
                table: "EmployeePayroll");

            migrationBuilder.AlterColumn<int>(
                name: "SalaryHeadId",
                table: "EmployeePayroll",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PaymentType",
                table: "EmployeePayroll",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "MonthlyAmount",
                table: "EmployeePayroll",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "HeadTypeId",
                table: "EmployeePayroll",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "EmployeePayroll",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<float>(
                name: "BasicPay",
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

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
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
                name: "SecurityDeductibles",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TrAllowance",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayroll_CurrencyDetails_CurrencyId",
                table: "EmployeePayroll",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayroll_SalaryHeadDetails_SalaryHeadId",
                table: "EmployeePayroll",
                column: "SalaryHeadId",
                principalTable: "SalaryHeadDetails",
                principalColumn: "SalaryHeadId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayroll_CurrencyDetails_CurrencyId",
                table: "EmployeePayroll");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayroll_SalaryHeadDetails_SalaryHeadId",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "BasicPay",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "CapacityBuildingDeductibles",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "CasualLeaveAllowance",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
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
                name: "SecurityDeductibles",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "TrAllowance",
                table: "EmployeePayroll");

            migrationBuilder.AlterColumn<int>(
                name: "SalaryHeadId",
                table: "EmployeePayroll",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentType",
                table: "EmployeePayroll",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MonthlyAmount",
                table: "EmployeePayroll",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HeadTypeId",
                table: "EmployeePayroll",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "EmployeePayroll",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayroll_CurrencyDetails_CurrencyId",
                table: "EmployeePayroll",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayroll_SalaryHeadDetails_SalaryHeadId",
                table: "EmployeePayroll",
                column: "SalaryHeadId",
                principalTable: "SalaryHeadDetails",
                principalColumn: "SalaryHeadId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

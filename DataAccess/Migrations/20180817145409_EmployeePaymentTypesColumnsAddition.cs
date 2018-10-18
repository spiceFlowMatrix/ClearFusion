using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeePaymentTypesColumnsAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Absent",
                table: "EmployeePaymentTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Attendance",
                table: "EmployeePaymentTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "EmployeePaymentTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficeCode",
                table: "EmployeePaymentTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayrollYear",
                table: "EmployeePaymentTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalDuration",
                table: "EmployeePaymentTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Absent",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropColumn(
                name: "Attendance",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropColumn(
                name: "OfficeCode",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropColumn(
                name: "PayrollYear",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropColumn(
                name: "TotalDuration",
                table: "EmployeePaymentTypes");
        }
    }
}

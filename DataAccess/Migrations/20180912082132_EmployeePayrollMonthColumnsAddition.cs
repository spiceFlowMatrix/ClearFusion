using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeePayrollMonthColumnsAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeadTypeId",
                table: "EmployeePayrollMonth",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "EmployeePayrollMonth",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadTypeId",
                table: "EmployeePayrollMonth");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "EmployeePayrollMonth");
        }
    }
}

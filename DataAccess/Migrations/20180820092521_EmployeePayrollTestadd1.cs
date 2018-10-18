using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeePayrollTestadd1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "basicpay",
                table: "EmployeePayrollDetailTest",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "TotalDeduction",
                table: "EmployeePayrollDetailTest",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "TotalAllowance",
                table: "EmployeePayrollDetailTest",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "basicpay",
                table: "EmployeePayrollDetailTest",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TotalDeduction",
                table: "EmployeePayrollDetailTest",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TotalAllowance",
                table: "EmployeePayrollDetailTest",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}

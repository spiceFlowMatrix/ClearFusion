using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class employeesalarytbalerelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountCode",
                table: "EmployeeSalaryAnalyticalInfo",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountCode",
                table: "EmployeeSalaryAnalyticalInfo",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

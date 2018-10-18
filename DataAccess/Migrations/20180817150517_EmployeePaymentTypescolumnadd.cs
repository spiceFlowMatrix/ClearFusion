using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeePaymentTypescolumnadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OfficeId",
                table: "EmployeePaymentTypes",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OfficeId",
                table: "EmployeePaymentTypes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class UpdatedTblEmployeeBasicSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeBasicSalaryDetail_CurrencyDetails_CurrencyId",
                table: "EmployeeBasicSalaryDetail");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "EmployeeBasicSalaryDetail",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<double>(
                name: "CapacityBuildingAmount",
                table: "EmployeeBasicSalaryDetail",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SecurityAmount",
                table: "EmployeeBasicSalaryDetail",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeBasicSalaryDetail_CurrencyDetails_CurrencyId",
                table: "EmployeeBasicSalaryDetail",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeBasicSalaryDetail_CurrencyDetails_CurrencyId",
                table: "EmployeeBasicSalaryDetail");

            migrationBuilder.DropColumn(
                name: "CapacityBuildingAmount",
                table: "EmployeeBasicSalaryDetail");

            migrationBuilder.DropColumn(
                name: "SecurityAmount",
                table: "EmployeeBasicSalaryDetail");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "EmployeeBasicSalaryDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeBasicSalaryDetail_CurrencyDetails_CurrencyId",
                table: "EmployeeBasicSalaryDetail",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

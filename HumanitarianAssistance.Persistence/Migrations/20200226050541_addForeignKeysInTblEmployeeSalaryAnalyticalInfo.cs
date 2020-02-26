using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addForeignKeysInTblEmployeeSalaryAnalyticalInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeBasicSalaryDetail_EmployeeId",
                table: "EmployeeBasicSalaryDetail");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBasicSalaryDetail_EmployeeId",
                table: "EmployeeBasicSalaryDetail",
                column: "EmployeeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployeeBasicSalaryDetail_EmployeeId",
                table: "EmployeeBasicSalaryDetail");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBasicSalaryDetail_EmployeeId",
                table: "EmployeeBasicSalaryDetail",
                column: "EmployeeId");
        }
    }
}

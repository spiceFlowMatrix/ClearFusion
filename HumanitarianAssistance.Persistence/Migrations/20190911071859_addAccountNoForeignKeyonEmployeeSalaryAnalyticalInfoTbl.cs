using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addAccountNoForeignKeyonEmployeeSalaryAnalyticalInfoTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_AccountNo",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "AccountNo");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaryAnalyticalInfo_ChartOfAccountNew_AccountNo",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "AccountNo",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaryAnalyticalInfo_ChartOfAccountNew_AccountNo",
                table: "EmployeeSalaryAnalyticalInfo");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_AccountNo",
                table: "EmployeeSalaryAnalyticalInfo");
        }
    }
}

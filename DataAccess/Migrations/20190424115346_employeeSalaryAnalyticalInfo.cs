using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class employeeSalaryAnalyticalInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaryAnalyticalInfo_ProjectBudgetLine_BudgetLineId",
                table: "EmployeeSalaryAnalyticalInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaryAnalyticalInfo_ProjectDetails_ProjectId",
                table: "EmployeeSalaryAnalyticalInfo");

            migrationBuilder.RenameColumn(
                name: "BudgetLineId",
                table: "EmployeeSalaryAnalyticalInfo",
                newName: "BudgetlineId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_BudgetLineId",
                table: "EmployeeSalaryAnalyticalInfo",
                newName: "IX_EmployeeSalaryAnalyticalInfo_BudgetlineId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaryAnalyticalInfo_ProjectBudgetLineDetail_BudgetlineId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "BudgetlineId",
                principalTable: "ProjectBudgetLineDetail",
                principalColumn: "BudgetLineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaryAnalyticalInfo_ProjectDetail_ProjectId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaryAnalyticalInfo_ProjectBudgetLineDetail_BudgetlineId",
                table: "EmployeeSalaryAnalyticalInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaryAnalyticalInfo_ProjectDetail_ProjectId",
                table: "EmployeeSalaryAnalyticalInfo");

            migrationBuilder.RenameColumn(
                name: "BudgetlineId",
                table: "EmployeeSalaryAnalyticalInfo",
                newName: "BudgetLineId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_BudgetlineId",
                table: "EmployeeSalaryAnalyticalInfo",
                newName: "IX_EmployeeSalaryAnalyticalInfo_BudgetLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaryAnalyticalInfo_ProjectBudgetLine_BudgetLineId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "BudgetLineId",
                principalTable: "ProjectBudgetLine",
                principalColumn: "BudgetLineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaryAnalyticalInfo_ProjectDetails_ProjectId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "ProjectId",
                principalTable: "ProjectDetails",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

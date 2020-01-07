using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addForeignKeyChartOfAccountId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectProposalDetail_ProjectId",
                table: "ProjectProposalDetail");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryHeadDetails_AccountNo",
                table: "SalaryHeadDetails",
                column: "AccountNo");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposalDetail_ProjectId",
                table: "ProjectProposalDetail",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollAccountHead_AccountNo",
                table: "EmployeePayrollAccountHead",
                column: "AccountNo");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayroll_AccountNo",
                table: "EmployeePayroll",
                column: "AccountNo");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayroll_ChartOfAccountNew_AccountNo",
                table: "EmployeePayroll",
                column: "AccountNo",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayrollAccountHead_ChartOfAccountNew_AccountNo",
                table: "EmployeePayrollAccountHead",
                column: "AccountNo",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryHeadDetails_ChartOfAccountNew_AccountNo",
                table: "SalaryHeadDetails",
                column: "AccountNo",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayroll_ChartOfAccountNew_AccountNo",
                table: "EmployeePayroll");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayrollAccountHead_ChartOfAccountNew_AccountNo",
                table: "EmployeePayrollAccountHead");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryHeadDetails_ChartOfAccountNew_AccountNo",
                table: "SalaryHeadDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalaryHeadDetails_AccountNo",
                table: "SalaryHeadDetails");

            migrationBuilder.DropIndex(
                name: "IX_ProjectProposalDetail_ProjectId",
                table: "ProjectProposalDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePayrollAccountHead_AccountNo",
                table: "EmployeePayrollAccountHead");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePayroll_AccountNo",
                table: "EmployeePayroll");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposalDetail_ProjectId",
                table: "ProjectProposalDetail",
                column: "ProjectId");
        }
    }
}

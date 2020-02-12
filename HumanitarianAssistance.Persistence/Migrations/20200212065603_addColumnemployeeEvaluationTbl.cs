using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addColumnemployeeEvaluationTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeAppraisalTeamMember",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalTeamMember_EmployeeId",
                table: "EmployeeAppraisalTeamMember",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAppraisalTeamMember_EmployeeDetail_EmployeeId",
                table: "EmployeeAppraisalTeamMember",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAppraisalTeamMember_EmployeeDetail_EmployeeId",
                table: "EmployeeAppraisalTeamMember");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAppraisalTeamMember_EmployeeId",
                table: "EmployeeAppraisalTeamMember");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeAppraisalTeamMember",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

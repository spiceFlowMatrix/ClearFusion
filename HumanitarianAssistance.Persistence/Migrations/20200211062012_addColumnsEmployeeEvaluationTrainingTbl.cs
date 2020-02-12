using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addColumnsEmployeeEvaluationTrainingTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatchLevel",
                table: "EmployeeEvaluationTraining",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Participated",
                table: "EmployeeEvaluationTraining",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RefresherTrm",
                table: "EmployeeEvaluationTraining",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrainingProgram",
                table: "EmployeeEvaluationTraining",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StrongandWeakPoints_EmployeeAppraisalDetailsId",
                table: "StrongandWeakPoints",
                column: "EmployeeAppraisalDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluationTraining_EmployeeAppraisalDetailsId",
                table: "EmployeeEvaluationTraining",
                column: "EmployeeAppraisalDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluation_EmployeeAppraisalDetailsId",
                table: "EmployeeEvaluation",
                column: "EmployeeAppraisalDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalTeamMember_EmployeeAppraisalDetailsId",
                table: "EmployeeAppraisalTeamMember",
                column: "EmployeeAppraisalDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAppraisalTeamMember_EmployeeAppraisalDetails_Employ~",
                table: "EmployeeAppraisalTeamMember",
                column: "EmployeeAppraisalDetailsId",
                principalTable: "EmployeeAppraisalDetails",
                principalColumn: "EmployeeAppraisalDetailsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEvaluation_EmployeeAppraisalDetails_EmployeeApprais~",
                table: "EmployeeEvaluation",
                column: "EmployeeAppraisalDetailsId",
                principalTable: "EmployeeAppraisalDetails",
                principalColumn: "EmployeeAppraisalDetailsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEvaluationTraining_EmployeeAppraisalDetails_Employe~",
                table: "EmployeeEvaluationTraining",
                column: "EmployeeAppraisalDetailsId",
                principalTable: "EmployeeAppraisalDetails",
                principalColumn: "EmployeeAppraisalDetailsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StrongandWeakPoints_EmployeeAppraisalDetails_EmployeeApprai~",
                table: "StrongandWeakPoints",
                column: "EmployeeAppraisalDetailsId",
                principalTable: "EmployeeAppraisalDetails",
                principalColumn: "EmployeeAppraisalDetailsId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAppraisalTeamMember_EmployeeAppraisalDetails_Employ~",
                table: "EmployeeAppraisalTeamMember");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEvaluation_EmployeeAppraisalDetails_EmployeeApprais~",
                table: "EmployeeEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEvaluationTraining_EmployeeAppraisalDetails_Employe~",
                table: "EmployeeEvaluationTraining");

            migrationBuilder.DropForeignKey(
                name: "FK_StrongandWeakPoints_EmployeeAppraisalDetails_EmployeeApprai~",
                table: "StrongandWeakPoints");

            migrationBuilder.DropIndex(
                name: "IX_StrongandWeakPoints_EmployeeAppraisalDetailsId",
                table: "StrongandWeakPoints");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeEvaluationTraining_EmployeeAppraisalDetailsId",
                table: "EmployeeEvaluationTraining");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeEvaluation_EmployeeAppraisalDetailsId",
                table: "EmployeeEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAppraisalTeamMember_EmployeeAppraisalDetailsId",
                table: "EmployeeAppraisalTeamMember");

            migrationBuilder.DropColumn(
                name: "CatchLevel",
                table: "EmployeeEvaluationTraining");

            migrationBuilder.DropColumn(
                name: "Participated",
                table: "EmployeeEvaluationTraining");

            migrationBuilder.DropColumn(
                name: "RefresherTrm",
                table: "EmployeeEvaluationTraining");

            migrationBuilder.DropColumn(
                name: "TrainingProgram",
                table: "EmployeeEvaluationTraining");
        }
    }
}

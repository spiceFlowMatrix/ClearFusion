using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addConstraintsOnAppraisalTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAppraisalDetails_EmployeeDetail_EmployeeId",
                table: "EmployeeAppraisalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAppraisalTeamMember_EmployeeAppraisalDetails_Employ~",
                table: "EmployeeAppraisalTeamMember");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEvaluation_EmployeeAppraisalDetails_EmployeeApprais~",
                table: "EmployeeEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEvaluation_EmployeeDetail_EmployeeId",
                table: "EmployeeEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEvaluationTraining_EmployeeAppraisalDetails_Employe~",
                table: "EmployeeEvaluationTraining");

            migrationBuilder.DropForeignKey(
                name: "FK_StrongandWeakPoints_EmployeeAppraisalDetails_EmployeeApprai~",
                table: "StrongandWeakPoints");

            migrationBuilder.DropIndex(
                name: "IX_ProjectSector_ProjectId",
                table: "ProjectSector");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeEvaluation_EmployeeAppraisalDetailsId",
                table: "EmployeeEvaluation");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeAppraisalDetailsId",
                table: "StrongandWeakPoints",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeAppraisalDetailsId",
                table: "EmployeeEvaluationTraining",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeEvaluation",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeAppraisalDetailsId",
                table: "EmployeeEvaluation",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeAppraisalDetailsId",
                table: "EmployeeAppraisalTeamMember",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "EmployeeAppraisalDetailsId",
                table: "EmployeeAppraisalQuestions",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeAppraisalDetails",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSector_ProjectId",
                table: "ProjectSector",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluation_EmployeeAppraisalDetailsId",
                table: "EmployeeEvaluation",
                column: "EmployeeAppraisalDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalQuestions_EmployeeAppraisalDetailsId",
                table: "EmployeeAppraisalQuestions",
                column: "EmployeeAppraisalDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAppraisalDetails_EmployeeDetail_EmployeeId",
                table: "EmployeeAppraisalDetails",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAppraisalQuestions_EmployeeAppraisalDetails_Employe~",
                table: "EmployeeAppraisalQuestions",
                column: "EmployeeAppraisalDetailsId",
                principalTable: "EmployeeAppraisalDetails",
                principalColumn: "EmployeeAppraisalDetailsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAppraisalTeamMember_EmployeeAppraisalDetails_Employ~",
                table: "EmployeeAppraisalTeamMember",
                column: "EmployeeAppraisalDetailsId",
                principalTable: "EmployeeAppraisalDetails",
                principalColumn: "EmployeeAppraisalDetailsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEvaluation_EmployeeAppraisalDetails_EmployeeApprais~",
                table: "EmployeeEvaluation",
                column: "EmployeeAppraisalDetailsId",
                principalTable: "EmployeeAppraisalDetails",
                principalColumn: "EmployeeAppraisalDetailsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEvaluation_EmployeeDetail_EmployeeId",
                table: "EmployeeEvaluation",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEvaluationTraining_EmployeeAppraisalDetails_Employe~",
                table: "EmployeeEvaluationTraining",
                column: "EmployeeAppraisalDetailsId",
                principalTable: "EmployeeAppraisalDetails",
                principalColumn: "EmployeeAppraisalDetailsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StrongandWeakPoints_EmployeeAppraisalDetails_EmployeeApprai~",
                table: "StrongandWeakPoints",
                column: "EmployeeAppraisalDetailsId",
                principalTable: "EmployeeAppraisalDetails",
                principalColumn: "EmployeeAppraisalDetailsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAppraisalDetails_EmployeeDetail_EmployeeId",
                table: "EmployeeAppraisalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAppraisalQuestions_EmployeeAppraisalDetails_Employe~",
                table: "EmployeeAppraisalQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAppraisalTeamMember_EmployeeAppraisalDetails_Employ~",
                table: "EmployeeAppraisalTeamMember");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEvaluation_EmployeeAppraisalDetails_EmployeeApprais~",
                table: "EmployeeEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEvaluation_EmployeeDetail_EmployeeId",
                table: "EmployeeEvaluation");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEvaluationTraining_EmployeeAppraisalDetails_Employe~",
                table: "EmployeeEvaluationTraining");

            migrationBuilder.DropForeignKey(
                name: "FK_StrongandWeakPoints_EmployeeAppraisalDetails_EmployeeApprai~",
                table: "StrongandWeakPoints");

            migrationBuilder.DropIndex(
                name: "IX_ProjectSector_ProjectId",
                table: "ProjectSector");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeEvaluation_EmployeeAppraisalDetailsId",
                table: "EmployeeEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAppraisalQuestions_EmployeeAppraisalDetailsId",
                table: "EmployeeAppraisalQuestions");

            migrationBuilder.DropColumn(
                name: "EmployeeAppraisalDetailsId",
                table: "EmployeeAppraisalQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeAppraisalDetailsId",
                table: "StrongandWeakPoints",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeAppraisalDetailsId",
                table: "EmployeeEvaluationTraining",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeEvaluation",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeAppraisalDetailsId",
                table: "EmployeeEvaluation",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeAppraisalDetailsId",
                table: "EmployeeAppraisalTeamMember",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeAppraisalDetails",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSector_ProjectId",
                table: "ProjectSector",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluation_EmployeeAppraisalDetailsId",
                table: "EmployeeEvaluation",
                column: "EmployeeAppraisalDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAppraisalDetails_EmployeeDetail_EmployeeId",
                table: "EmployeeAppraisalDetails",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_EmployeeEvaluation_EmployeeDetail_EmployeeId",
                table: "EmployeeEvaluation",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
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
    }
}

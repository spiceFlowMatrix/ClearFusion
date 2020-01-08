using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class projectMonitoringIndicatorQuestionsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMonitoringIndicatorQuestions_ProjectMonitoringIndica~",
                table: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMonitoringIndicatorQuestions_ProjectIndicatorQuestio~",
                table: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_QuestionId",
                table: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.DropColumn(
                name: "VerificationId",
                table: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.RenameColumn(
                name: "Verification",
                table: "ProjectMonitoringIndicatorQuestions",
                newName: "VerificationSourceName");

            migrationBuilder.AlterColumn<long>(
                name: "MonitoringIndicatorId",
                table: "ProjectMonitoringIndicatorQuestions",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "IndicatorQuestionId",
                table: "ProjectMonitoringIndicatorQuestions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VerificationSourceId",
                table: "ProjectMonitoringIndicatorQuestions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_IndicatorQuestionId",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "IndicatorQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMonitoringIndicatorQuestions_ProjectIndicatorQuestio~",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "IndicatorQuestionId",
                principalTable: "ProjectIndicatorQuestions",
                principalColumn: "IndicatorQuestionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMonitoringIndicatorQuestions_ProjectMonitoringIndica~",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "MonitoringIndicatorId",
                principalTable: "ProjectMonitoringIndicatorDetail",
                principalColumn: "MonitoringIndicatorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMonitoringIndicatorQuestions_ProjectIndicatorQuestio~",
                table: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMonitoringIndicatorQuestions_ProjectMonitoringIndica~",
                table: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_IndicatorQuestionId",
                table: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.DropColumn(
                name: "IndicatorQuestionId",
                table: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.DropColumn(
                name: "VerificationSourceId",
                table: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.RenameColumn(
                name: "VerificationSourceName",
                table: "ProjectMonitoringIndicatorQuestions",
                newName: "Verification");

            migrationBuilder.AlterColumn<long>(
                name: "MonitoringIndicatorId",
                table: "ProjectMonitoringIndicatorQuestions",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "QuestionId",
                table: "ProjectMonitoringIndicatorQuestions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "VerificationId",
                table: "ProjectMonitoringIndicatorQuestions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_QuestionId",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMonitoringIndicatorQuestions_ProjectMonitoringIndica~",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "MonitoringIndicatorId",
                principalTable: "ProjectMonitoringIndicatorDetail",
                principalColumn: "MonitoringIndicatorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMonitoringIndicatorQuestions_ProjectIndicatorQuestio~",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "QuestionId",
                principalTable: "ProjectIndicatorQuestions",
                principalColumn: "IndicatorQuestionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

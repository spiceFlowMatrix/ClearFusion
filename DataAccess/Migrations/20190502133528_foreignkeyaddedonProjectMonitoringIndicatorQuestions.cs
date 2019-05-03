using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class foreignkeyaddedonProjectMonitoringIndicatorQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_QuestionId",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMonitoringIndicatorQuestions_ProjectIndicatorQuestions_QuestionId",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "QuestionId",
                principalTable: "ProjectIndicatorQuestions",
                principalColumn: "IndicatorQuestionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMonitoringIndicatorQuestions_ProjectIndicatorQuestions_QuestionId",
                table: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_QuestionId",
                table: "ProjectMonitoringIndicatorQuestions");
        }
    }
}

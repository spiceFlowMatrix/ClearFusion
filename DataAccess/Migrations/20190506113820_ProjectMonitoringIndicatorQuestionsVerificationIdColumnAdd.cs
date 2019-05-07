using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ProjectMonitoringIndicatorQuestionsVerificationIdColumnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verification",
                table: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.AddColumn<int>(
                name: "VerificationId",
                table: "ProjectMonitoringIndicatorQuestions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationId",
                table: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.AddColumn<string>(
                name: "Verification",
                table: "ProjectMonitoringIndicatorQuestions",
                nullable: true);
        }
    }
}

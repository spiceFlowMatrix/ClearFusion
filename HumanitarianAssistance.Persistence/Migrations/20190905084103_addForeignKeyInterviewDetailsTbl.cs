using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addForeignKeyInterviewDetailsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "InterviewDetails");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewDetails_JobId",
                table: "InterviewDetails",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewDetails_JobHiringDetails_JobId",
                table: "InterviewDetails",
                column: "JobId",
                principalTable: "JobHiringDetails",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterviewDetails_JobHiringDetails_JobId",
                table: "InterviewDetails");

            migrationBuilder.DropIndex(
                name: "IX_InterviewDetails_JobId",
                table: "InterviewDetails");

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "InterviewDetails",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateAndRemoveColumnsInCandidateDetailsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "IsShortlisted",
                table: "CandidateDetails");

            migrationBuilder.AddColumn<int>(
                name: "CandidateStatus",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "InterviewId",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CandidateStatus",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "InterviewId",
                table: "CandidateDetails");

            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShortlisted",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: false);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateCoulmnsInCandidateDetailsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExperienceInMonth",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "ExperienceInYear",
                table: "CandidateDetails");

            migrationBuilder.RenameColumn(
                name: "IsShortListed",
                table: "CandidateDetails",
                newName: "IsShortlisted");

            migrationBuilder.AddColumn<double>(
                name: "IrrelevantExperienceInYear",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RelevantExperienceInYear",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalExperienceInYear",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IrrelevantExperienceInYear",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "RelevantExperienceInYear",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "TotalExperienceInYear",
                table: "CandidateDetails");

            migrationBuilder.RenameColumn(
                name: "IsShortlisted",
                table: "CandidateDetails",
                newName: "IsShortListed");

            migrationBuilder.AddColumn<int>(
                name: "ExperienceInMonth",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExperienceInYear",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0);
        }
    }
}

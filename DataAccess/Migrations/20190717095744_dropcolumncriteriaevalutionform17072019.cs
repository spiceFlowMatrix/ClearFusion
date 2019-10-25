using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class dropcolumncriteriaevalutionform17072019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aggriculture",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Awareness",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Children",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "CommunityDevelopment",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "DRR",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Documentaries",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "DrugAbuses",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "HealthAndNutrition",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "InvestigativeJournalism",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Music",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "News",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "OtherActivity",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Others",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "PrintedMedia",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "RadioProduction",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Reports",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Right",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "RoundTable",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "ServiceEducation",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "ServiceHealthAndNutrition",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "SocioPolitiacalDebate",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Studies",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "TVProgram",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Women",
                table: "PurposeofInitiativeCriteria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aggriculture",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Awareness",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Children",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CommunityDevelopment",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Culture",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DRR",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Documentaries",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DrugAbuses",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Education",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HealthAndNutrition",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InvestigativeJournalism",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Music",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "News",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherActivity",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Others",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PrintedMedia",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RadioProduction",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Reports",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Right",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RoundTable",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ServiceEducation",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ServiceHealthAndNutrition",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SocioPolitiacalDebate",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Studies",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TVProgram",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Women",
                table: "PurposeofInitiativeCriteria",
                nullable: true);
        }
    }
}

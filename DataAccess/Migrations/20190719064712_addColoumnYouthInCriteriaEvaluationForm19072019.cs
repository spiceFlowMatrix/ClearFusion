using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addColoumnYouthInCriteriaEvaluationForm19072019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Men",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Women",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Youth",
                table: "PurposeofInitiativeCriteria",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Men",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Women",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Youth",
                table: "PurposeofInitiativeCriteria");
        }
    }
}

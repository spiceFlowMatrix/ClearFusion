using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ProjectDetailDirectorReviewerIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DirectorId",
                table: "ProjectDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewerId",
                table: "ProjectDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "ProjectDetail");

            migrationBuilder.DropColumn(
                name: "ReviewerId",
                table: "ProjectDetail");
        }
    }
}

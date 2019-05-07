using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addFieldTargetAndAchievedProjectActivitydetail06052019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Achieved",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Target",
                table: "ProjectActivityDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Achieved",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "ProjectActivityDetail");
        }
    }
}

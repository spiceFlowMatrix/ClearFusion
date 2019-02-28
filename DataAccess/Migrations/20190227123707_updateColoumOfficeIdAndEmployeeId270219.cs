using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateColoumOfficeIdAndEmployeeId270219 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "ProjectActivityDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssigneeId",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "ProjectActivityDetail",
                nullable: true);
        }
    }
}

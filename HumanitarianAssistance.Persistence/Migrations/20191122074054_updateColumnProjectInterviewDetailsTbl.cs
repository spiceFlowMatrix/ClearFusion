using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateColumnProjectInterviewDetailsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalMarksObtained",
                table: "ProjectInterviewDetails");

            migrationBuilder.AddColumn<int>(
                name: "TotalMarksObtain",
                table: "ProjectInterviewDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalMarksObtain",
                table: "ProjectInterviewDetails");

            migrationBuilder.AddColumn<string>(
                name: "TotalMarksObtained",
                table: "ProjectInterviewDetails",
                nullable: true);
        }
    }
}

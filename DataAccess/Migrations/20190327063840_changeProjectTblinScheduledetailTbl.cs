using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class changeProjectTblinScheduledetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDetails_ProjectDetails_ProjectId",
                table: "ScheduleDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDetails_ProjectDetail_ProjectId",
                table: "ScheduleDetails",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDetails_ProjectDetail_ProjectId",
                table: "ScheduleDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDetails_ProjectDetails_ProjectId",
                table: "ScheduleDetails",
                column: "ProjectId",
                principalTable: "ProjectDetails",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

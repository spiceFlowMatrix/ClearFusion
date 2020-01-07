using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addColumnActivityIdInProjectMonitoringReviewDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringReviewDetail_ActivityId",
                table: "ProjectMonitoringReviewDetail",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMonitoringReviewDetail_ProjectActivityDetail_Activit~",
                table: "ProjectMonitoringReviewDetail",
                column: "ActivityId",
                principalTable: "ProjectActivityDetail",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMonitoringReviewDetail_ProjectActivityDetail_Activit~",
                table: "ProjectMonitoringReviewDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMonitoringReviewDetail_ActivityId",
                table: "ProjectMonitoringReviewDetail");
        }
    }
}

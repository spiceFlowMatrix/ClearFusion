using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addRelationActivityIdInProjectMonitoringReviewDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectOtherDetail_ProjectId",
                table: "ProjectOtherDetail");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOtherDetail_ProjectId",
                table: "ProjectOtherDetail",
                column: "ProjectId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectOtherDetail_ProjectId",
                table: "ProjectOtherDetail");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOtherDetail_ProjectId",
                table: "ProjectOtherDetail",
                column: "ProjectId");
        }
    }
}

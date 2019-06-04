using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ProjectMonitoringIndicatorDetailForeignKeyAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorDetail_ProjectIndicatorId",
                table: "ProjectMonitoringIndicatorDetail",
                column: "ProjectIndicatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMonitoringIndicatorDetail_ProjectIndicators_ProjectIndicatorId",
                table: "ProjectMonitoringIndicatorDetail",
                column: "ProjectIndicatorId",
                principalTable: "ProjectIndicators",
                principalColumn: "ProjectIndicatorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMonitoringIndicatorDetail_ProjectIndicators_ProjectIndicatorId",
                table: "ProjectMonitoringIndicatorDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMonitoringIndicatorDetail_ProjectIndicatorId",
                table: "ProjectMonitoringIndicatorDetail");
        }
    }
}

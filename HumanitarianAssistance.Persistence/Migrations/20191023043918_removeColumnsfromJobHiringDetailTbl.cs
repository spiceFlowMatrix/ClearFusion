using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class removeColumnsfromJobHiringDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobHiringDetails_HiringRequestId",
                table: "JobHiringDetails");

            migrationBuilder.DropColumn(
                name: "JobCategory",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetails_HiringRequestId",
                table: "JobHiringDetails",
                column: "HiringRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobHiringDetails_HiringRequestId",
                table: "JobHiringDetails");

            migrationBuilder.AddColumn<string>(
                name: "JobCategory",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetails_HiringRequestId",
                table: "JobHiringDetails",
                column: "HiringRequestId",
                unique: true);
        }
    }
}

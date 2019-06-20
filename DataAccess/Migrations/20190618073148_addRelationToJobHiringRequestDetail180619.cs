using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addRelationToJobHiringRequestDetail180619 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "HiringRequestId",
                table: "JobHiringDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetails_HiringRequestId",
                table: "JobHiringDetails",
                column: "HiringRequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobHiringDetails_ProjectHiringRequestDetail_HiringRequestId",
                table: "JobHiringDetails",
                column: "HiringRequestId",
                principalTable: "ProjectHiringRequestDetail",
                principalColumn: "HiringRequestId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobHiringDetails_ProjectHiringRequestDetail_HiringRequestId",
                table: "JobHiringDetails");

            migrationBuilder.DropIndex(
                name: "IX_JobHiringDetails_HiringRequestId",
                table: "JobHiringDetails");

            migrationBuilder.DropColumn(
                name: "HiringRequestId",
                table: "JobHiringDetails");
        }
    }
}

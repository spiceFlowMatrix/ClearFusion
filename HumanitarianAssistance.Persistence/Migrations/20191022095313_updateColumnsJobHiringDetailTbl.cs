using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateColumnsJobHiringDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "JobHiringDetail",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetail_ProfessionId",
                table: "JobHiringDetail",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetail_ProjectId",
                table: "JobHiringDetail",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobHiringDetail_ProfessionDetails_ProfessionId",
                table: "JobHiringDetail",
                column: "ProfessionId",
                principalTable: "ProfessionDetails",
                principalColumn: "ProfessionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobHiringDetail_ProjectDetail_ProjectId",
                table: "JobHiringDetail",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobHiringDetail_ProfessionDetails_ProfessionId",
                table: "JobHiringDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_JobHiringDetail_ProjectDetail_ProjectId",
                table: "JobHiringDetail");

            migrationBuilder.DropIndex(
                name: "IX_JobHiringDetail_ProfessionId",
                table: "JobHiringDetail");

            migrationBuilder.DropIndex(
                name: "IX_JobHiringDetail_ProjectId",
                table: "JobHiringDetail");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "JobHiringDetail");
        }
    }
}

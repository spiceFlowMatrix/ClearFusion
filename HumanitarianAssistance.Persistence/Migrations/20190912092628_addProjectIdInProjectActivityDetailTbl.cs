using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addProjectIdInProjectActivityDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_ProjectId",
                table: "ProjectActivityDetail",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActivityDetail_ProjectDetail_ProjectId",
                table: "ProjectActivityDetail",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActivityDetail_ProjectDetail_ProjectId",
                table: "ProjectActivityDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectActivityDetail_ProjectId",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectActivityDetail");
        }
    }
}

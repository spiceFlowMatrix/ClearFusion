using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ProjectActivityDetailSelfRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_ParentId",
                table: "ProjectActivityDetail",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActivityDetail_ProjectActivityDetail_ParentId",
                table: "ProjectActivityDetail",
                column: "ParentId",
                principalTable: "ProjectActivityDetail",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActivityDetail_ProjectActivityDetail_ParentId",
                table: "ProjectActivityDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectActivityDetail_ParentId",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ProjectActivityDetail");
        }
    }
}

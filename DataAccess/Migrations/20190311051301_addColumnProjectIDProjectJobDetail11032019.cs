using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addColumnProjectIDProjectJobDetail11032019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "ProjectJobDetail",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobDetail_ProjectId",
                table: "ProjectJobDetail",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectJobDetail_ProjectDetail_ProjectId",
                table: "ProjectJobDetail",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectJobDetail_ProjectDetail_ProjectId",
                table: "ProjectJobDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectJobDetail_ProjectId",
                table: "ProjectJobDetail");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectJobDetail");
        }
    }
}

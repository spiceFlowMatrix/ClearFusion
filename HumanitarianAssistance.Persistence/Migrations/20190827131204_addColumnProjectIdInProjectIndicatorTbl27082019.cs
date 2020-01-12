using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addColumnProjectIdInProjectIndicatorTbl27082019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "ProjectIndicators",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIndicators_ProjectId",
                table: "ProjectIndicators",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectIndicators_ProjectDetail_ProjectId",
                table: "ProjectIndicators",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectIndicators_ProjectDetail_ProjectId",
                table: "ProjectIndicators");

            migrationBuilder.DropIndex(
                name: "IX_ProjectIndicators_ProjectId",
                table: "ProjectIndicators");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectIndicators");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addColumnProjectSectorAndProgramTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectSector_ProjectId",
                table: "ProjectSector");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSector_ProjectId",
                table: "ProjectSector",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectSector_ProjectId",
                table: "ProjectSector");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSector_ProjectId",
                table: "ProjectSector",
                column: "ProjectId",
                unique: true);
        }
    }
}

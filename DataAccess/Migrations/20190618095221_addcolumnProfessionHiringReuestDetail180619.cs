using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addcolumnProfessionHiringReuestDetail180619 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profession",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_ProfessionId",
                table: "ProjectHiringRequestDetail",
                column: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHiringRequestDetail_ProfessionDetails_ProfessionId",
                table: "ProjectHiringRequestDetail",
                column: "ProfessionId",
                principalTable: "ProfessionDetails",
                principalColumn: "ProfessionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHiringRequestDetail_ProfessionDetails_ProfessionId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectHiringRequestDetail_ProfessionId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "ProjectHiringRequestDetail",
                nullable: true);
        }
    }
}

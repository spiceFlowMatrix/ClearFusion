using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updatePlcyTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProducerId",
                table: "PolicyDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PolicyDetails_ProducerId",
                table: "PolicyDetails",
                column: "ProducerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyDetails_Producers_ProducerId",
                table: "PolicyDetails",
                column: "ProducerId",
                principalTable: "Producers",
                principalColumn: "ProducerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PolicyDetails_Producers_ProducerId",
                table: "PolicyDetails");

            migrationBuilder.DropIndex(
                name: "IX_PolicyDetails_ProducerId",
                table: "PolicyDetails");

            migrationBuilder.DropColumn(
                name: "ProducerId",
                table: "PolicyDetails");
        }
    }
}

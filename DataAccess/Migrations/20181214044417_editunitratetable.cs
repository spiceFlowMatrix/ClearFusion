using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class editunitratetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MediaCategoryId",
                table: "UnitRates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitRates_MediaCategoryId",
                table: "UnitRates",
                column: "MediaCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitRates_MediaCategories_MediaCategoryId",
                table: "UnitRates",
                column: "MediaCategoryId",
                principalTable: "MediaCategories",
                principalColumn: "MediaCategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitRates_MediaCategories_MediaCategoryId",
                table: "UnitRates");

            migrationBuilder.DropIndex(
                name: "IX_UnitRates_MediaCategoryId",
                table: "UnitRates");

            migrationBuilder.DropColumn(
                name: "MediaCategoryId",
                table: "UnitRates");
        }
    }
}

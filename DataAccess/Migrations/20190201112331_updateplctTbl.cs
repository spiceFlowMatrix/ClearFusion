using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateplctTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PolicyDetails_Languages_LanguageId",
                table: "PolicyDetails");

            migrationBuilder.DropIndex(
                name: "IX_PolicyDetails_LanguageId",
                table: "PolicyDetails");

            migrationBuilder.AddColumn<int>(
                name: "LanguagesLanguageId",
                table: "PolicyDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PolicyDetails_LanguagesLanguageId",
                table: "PolicyDetails",
                column: "LanguagesLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyDetails_LanguageDetail_LanguagesLanguageId",
                table: "PolicyDetails",
                column: "LanguagesLanguageId",
                principalTable: "LanguageDetail",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PolicyDetails_LanguageDetail_LanguagesLanguageId",
                table: "PolicyDetails");

            migrationBuilder.DropIndex(
                name: "IX_PolicyDetails_LanguagesLanguageId",
                table: "PolicyDetails");

            migrationBuilder.DropColumn(
                name: "LanguagesLanguageId",
                table: "PolicyDetails");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyDetails_LanguageId",
                table: "PolicyDetails",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyDetails_Languages_LanguageId",
                table: "PolicyDetails",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateContractDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractDetails_Languages_LanguageId",
                table: "ContractDetails");

            migrationBuilder.DropIndex(
                name: "IX_ContractDetails_LanguageId",
                table: "ContractDetails");

            migrationBuilder.AddColumn<int>(
                name: "LanguageDetailLanguageId",
                table: "ContractDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_LanguageDetailLanguageId",
                table: "ContractDetails",
                column: "LanguageDetailLanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractDetails_LanguageDetail_LanguageDetailLanguageId",
                table: "ContractDetails",
                column: "LanguageDetailLanguageId",
                principalTable: "LanguageDetail",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractDetails_LanguageDetail_LanguageDetailLanguageId",
                table: "ContractDetails");

            migrationBuilder.DropIndex(
                name: "IX_ContractDetails_LanguageDetailLanguageId",
                table: "ContractDetails");

            migrationBuilder.DropColumn(
                name: "LanguageDetailLanguageId",
                table: "ContractDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_LanguageId",
                table: "ContractDetails",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractDetails_Languages_LanguageId",
                table: "ContractDetails",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

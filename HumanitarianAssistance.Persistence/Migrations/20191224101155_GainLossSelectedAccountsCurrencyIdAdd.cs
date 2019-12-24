using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class GainLossSelectedAccountsCurrencyIdAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "GainLossSelectedAccounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_CurrencyId",
                table: "GainLossSelectedAccounts",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_GainLossSelectedAccounts_CurrencyDetails_CurrencyId",
                table: "GainLossSelectedAccounts",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GainLossSelectedAccounts_CurrencyDetails_CurrencyId",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropIndex(
                name: "IX_GainLossSelectedAccounts_CurrencyId",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "GainLossSelectedAccounts");
        }
    }
}

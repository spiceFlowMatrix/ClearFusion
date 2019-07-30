using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class currencyIdOnRiskCriteriaDetailPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Culture",
                table: "RiskCriteriaDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "RiskCriteriaDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ethnicity",
                table: "RiskCriteriaDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Geographical",
                table: "RiskCriteriaDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Insecurity",
                table: "RiskCriteriaDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReligiousBeliefs",
                table: "RiskCriteriaDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Season",
                table: "RiskCriteriaDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteriaDetail_CurrencyId",
                table: "RiskCriteriaDetail",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RiskCriteriaDetail_CurrencyDetails_CurrencyId",
                table: "RiskCriteriaDetail",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RiskCriteriaDetail_CurrencyDetails_CurrencyId",
                table: "RiskCriteriaDetail");

            migrationBuilder.DropIndex(
                name: "IX_RiskCriteriaDetail_CurrencyId",
                table: "RiskCriteriaDetail");

            migrationBuilder.DropColumn(
                name: "Culture",
                table: "RiskCriteriaDetail");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "RiskCriteriaDetail");

            migrationBuilder.DropColumn(
                name: "Ethnicity",
                table: "RiskCriteriaDetail");

            migrationBuilder.DropColumn(
                name: "Geographical",
                table: "RiskCriteriaDetail");

            migrationBuilder.DropColumn(
                name: "Insecurity",
                table: "RiskCriteriaDetail");

            migrationBuilder.DropColumn(
                name: "ReligiousBeliefs",
                table: "RiskCriteriaDetail");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "RiskCriteriaDetail");
        }
    }
}

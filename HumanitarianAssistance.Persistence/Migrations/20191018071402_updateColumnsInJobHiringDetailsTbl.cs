using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateColumnsInJobHiringDetailsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "JobHiringDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "JobHiringDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetails_CurrencyId",
                table: "JobHiringDetails",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobHiringDetails_CurrencyDetails_CurrencyId",
                table: "JobHiringDetails",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobHiringDetails_CurrencyDetails_CurrencyId",
                table: "JobHiringDetails");

            migrationBuilder.DropIndex(
                name: "IX_JobHiringDetails_CurrencyId",
                table: "JobHiringDetails");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "JobHiringDetails");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "JobHiringDetails");
        }
    }
}

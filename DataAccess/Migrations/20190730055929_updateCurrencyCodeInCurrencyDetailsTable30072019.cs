using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateCurrencyCodeInCurrencyDetailsTable30072019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CurrencyDetails",
                keyColumn: "CurrencyId",
                keyValue: 1,
                column: "CurrencyCode",
                value: "AFN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CurrencyDetails",
                keyColumn: "CurrencyId",
                keyValue: 1,
                column: "CurrencyCode",
                value: "AFG");
        }
    }
}

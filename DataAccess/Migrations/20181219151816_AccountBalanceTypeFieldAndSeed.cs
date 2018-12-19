using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AccountBalanceTypeFieldAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCreditBalancetype",
                table: "ChartOfAccountNew",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCreditBalancetype",
                table: "AccountHeadType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AccountHeadType",
                keyColumn: "AccountHeadTypeId",
                keyValue: 2,
                column: "IsCreditBalancetype",
                value: true);

            migrationBuilder.UpdateData(
                table: "AccountHeadType",
                keyColumn: "AccountHeadTypeId",
                keyValue: 3,
                column: "IsCreditBalancetype",
                value: true);

            migrationBuilder.UpdateData(
                table: "AccountHeadType",
                keyColumn: "AccountHeadTypeId",
                keyValue: 4,
                column: "IsCreditBalancetype",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCreditBalancetype",
                table: "ChartOfAccountNew");

            migrationBuilder.DropColumn(
                name: "IsCreditBalancetype",
                table: "AccountHeadType");
        }
    }
}

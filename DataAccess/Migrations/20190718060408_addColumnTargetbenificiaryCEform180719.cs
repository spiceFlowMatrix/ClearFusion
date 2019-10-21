using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addColumnTargetbenificiaryCEform180719 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Children",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IDPs",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Kuchis",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Returnees",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Widows",
                table: "PurposeofInitiativeCriteria",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Children",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "IDPs",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Kuchis",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Returnees",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Widows",
                table: "PurposeofInitiativeCriteria");
        }
    }
}

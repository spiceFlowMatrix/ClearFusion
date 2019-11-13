using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class dropcolumntargetbenificiaryCEform180719 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Product",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Service",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "TargetBenificaiaryWomen",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "TargetBenificiaryAgeGroup",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "TargetBenificiaryMen",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "TargetBenificiaryaOccupation",
                table: "PurposeofInitiativeCriteria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Product",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Service",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TargetBenificaiaryWomen",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TargetBenificiaryAgeGroup",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TargetBenificiaryMen",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TargetBenificiaryaOccupation",
                table: "PurposeofInitiativeCriteria",
                nullable: true);
        }
    }
}

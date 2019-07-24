using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addColumnProvinceMultiSelect18072019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CountryMultiSelectId",
                table: "ProvinceMultiSelect",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMultiSelect_CountryMultiSelectId",
                table: "ProvinceMultiSelect",
                column: "CountryMultiSelectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceMultiSelect_CountryMultiSelectDetails_CountryMultiSelectId",
                table: "ProvinceMultiSelect",
                column: "CountryMultiSelectId",
                principalTable: "CountryMultiSelectDetails",
                principalColumn: "CountryMultiSelectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceMultiSelect_CountryMultiSelectDetails_CountryMultiSelectId",
                table: "ProvinceMultiSelect");

            migrationBuilder.DropIndex(
                name: "IX_ProvinceMultiSelect_CountryMultiSelectId",
                table: "ProvinceMultiSelect");

            migrationBuilder.DropColumn(
                name: "CountryMultiSelectId",
                table: "ProvinceMultiSelect");
        }
    }
}

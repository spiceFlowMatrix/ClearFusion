using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateColumnDeleteCountyMultiSelectinProvinceMultiSelect19072019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceMultiSelect_CountryMultiSelectDetails_CountryMultiSelectId",
                table: "ProvinceMultiSelect");

            migrationBuilder.AlterColumn<long>(
                name: "CountryMultiSelectId",
                table: "ProvinceMultiSelect",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceMultiSelect_CountryMultiSelectDetails_CountryMultiSelectId",
                table: "ProvinceMultiSelect",
                column: "CountryMultiSelectId",
                principalTable: "CountryMultiSelectDetails",
                principalColumn: "CountryMultiSelectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceMultiSelect_CountryMultiSelectDetails_CountryMultiSelectId",
                table: "ProvinceMultiSelect");

            migrationBuilder.AlterColumn<long>(
                name: "CountryMultiSelectId",
                table: "ProvinceMultiSelect",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceMultiSelect_CountryMultiSelectDetails_CountryMultiSelectId",
                table: "ProvinceMultiSelect",
                column: "CountryMultiSelectId",
                principalTable: "CountryMultiSelectDetails",
                principalColumn: "CountryMultiSelectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

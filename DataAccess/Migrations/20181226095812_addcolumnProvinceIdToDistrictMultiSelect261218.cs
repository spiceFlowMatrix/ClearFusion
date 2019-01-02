using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addcolumnProvinceIdToDistrictMultiSelect261218 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "DistrictMultiSelect",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMultiSelect_ProvinceId",
                table: "DistrictMultiSelect",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DistrictMultiSelect_ProvinceDetails_ProvinceId",
                table: "DistrictMultiSelect",
                column: "ProvinceId",
                principalTable: "ProvinceDetails",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DistrictMultiSelect_ProvinceDetails_ProvinceId",
                table: "DistrictMultiSelect");

            migrationBuilder.DropIndex(
                name: "IX_DistrictMultiSelect_ProvinceId",
                table: "DistrictMultiSelect");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "DistrictMultiSelect");
        }
    }
}

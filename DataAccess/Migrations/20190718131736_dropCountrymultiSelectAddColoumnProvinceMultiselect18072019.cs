using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class dropCountrymultiSelectAddColoumnProvinceMultiselect18072019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryMultiSelectDetails_CountryDetails_CountryId",
                table: "CountryMultiSelectDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryMultiSelectDetails_ProjectDetail_ProjectId",
                table: "CountryMultiSelectDetails");

            migrationBuilder.DropIndex(
                name: "IX_CountryMultiSelectDetails_CountryId",
                table: "CountryMultiSelectDetails");

            migrationBuilder.DropIndex(
                name: "IX_CountryMultiSelectDetails_ProjectId",
                table: "CountryMultiSelectDetails");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "CountryMultiSelectDetails");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "CountryMultiSelectDetails");

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "ProvinceMultiSelect",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "ProvinceMultiSelect",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMultiSelect_ProjectId",
                table: "ProvinceMultiSelect",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMultiSelect_ProvinceId",
                table: "ProvinceMultiSelect",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceMultiSelect_ProjectDetail_ProjectId",
                table: "ProvinceMultiSelect",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceMultiSelect_ProvinceDetails_ProvinceId",
                table: "ProvinceMultiSelect",
                column: "ProvinceId",
                principalTable: "ProvinceDetails",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceMultiSelect_ProjectDetail_ProjectId",
                table: "ProvinceMultiSelect");

            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceMultiSelect_ProvinceDetails_ProvinceId",
                table: "ProvinceMultiSelect");

            migrationBuilder.DropIndex(
                name: "IX_ProvinceMultiSelect_ProjectId",
                table: "ProvinceMultiSelect");

            migrationBuilder.DropIndex(
                name: "IX_ProvinceMultiSelect_ProvinceId",
                table: "ProvinceMultiSelect");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProvinceMultiSelect");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "ProvinceMultiSelect");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "CountryMultiSelectDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "CountryMultiSelectDetails",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CountryMultiSelectDetails_CountryId",
                table: "CountryMultiSelectDetails",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryMultiSelectDetails_ProjectId",
                table: "CountryMultiSelectDetails",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryMultiSelectDetails_CountryDetails_CountryId",
                table: "CountryMultiSelectDetails",
                column: "CountryId",
                principalTable: "CountryDetails",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryMultiSelectDetails_ProjectDetail_ProjectId",
                table: "CountryMultiSelectDetails",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

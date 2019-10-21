using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class dropcolumnprovincemultiselectdetails18072019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

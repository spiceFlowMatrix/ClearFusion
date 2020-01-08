using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addCountryIdInProjectActivityProvinceDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "ProjectActivityProvinceDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityProvinceDetail_CountryId",
                table: "ProjectActivityProvinceDetail",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_CountryId",
                table: "ProjectActivityDetail",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActivityDetail_CountryDetails_CountryId",
                table: "ProjectActivityDetail",
                column: "CountryId",
                principalTable: "CountryDetails",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActivityProvinceDetail_CountryDetails_CountryId",
                table: "ProjectActivityProvinceDetail",
                column: "CountryId",
                principalTable: "CountryDetails",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActivityDetail_CountryDetails_CountryId",
                table: "ProjectActivityDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActivityProvinceDetail_CountryDetails_CountryId",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectActivityProvinceDetail_CountryId",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectActivityDetail_CountryId",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "ProjectActivityDetail");
        }
    }
}

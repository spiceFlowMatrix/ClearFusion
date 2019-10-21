using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addColumnCountryMultiSelectAddColoumn18072019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "CountryMultiSelectDetails",
                nullable: true);

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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryMultiSelectDetails_ProjectDetail_ProjectId",
                table: "CountryMultiSelectDetails",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ApplicationPagesforignkeyremovefromPermissionsInRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionsInRoles_ApplicationPages_PageId",
                table: "PermissionsInRoles");

            migrationBuilder.DropIndex(
                name: "IX_PermissionsInRoles_PageId",
                table: "PermissionsInRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PermissionsInRoles_PageId",
                table: "PermissionsInRoles",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionsInRoles_ApplicationPages_PageId",
                table: "PermissionsInRoles",
                column: "PageId",
                principalTable: "ApplicationPages",
                principalColumn: "PageId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

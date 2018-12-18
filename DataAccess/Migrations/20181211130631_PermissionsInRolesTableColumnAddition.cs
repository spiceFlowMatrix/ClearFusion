using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class PermissionsInRolesTableColumnAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanEdit",
                table: "PermissionsInRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanView",
                table: "PermissionsInRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "PermissionsInRoles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PageId",
                table: "PermissionsInRoles",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionsInRoles_ApplicationPages_PageId",
                table: "PermissionsInRoles");

            migrationBuilder.DropIndex(
                name: "IX_PermissionsInRoles_PageId",
                table: "PermissionsInRoles");

            migrationBuilder.DropColumn(
                name: "CanEdit",
                table: "PermissionsInRoles");

            migrationBuilder.DropColumn(
                name: "CanView",
                table: "PermissionsInRoles");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "PermissionsInRoles");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "PermissionsInRoles");
        }
    }
}

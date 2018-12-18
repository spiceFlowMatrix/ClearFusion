using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class RolePermissionstableadd1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    RolesPermissionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<string>(nullable: false),
                    IsGrant = table.Column<bool>(nullable: false),
                    CurrentPermissionId = table.Column<string>(nullable: true),
                    PageId = table.Column<int>(nullable: true),
                    ModuleId = table.Column<int>(nullable: false),
                    CanView = table.Column<bool>(nullable: false),
                    CanEdit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.RoleId);
                    table.UniqueConstraint("AK_RolePermissions_RoleId_RolesPermissionId", x => new { x.RoleId, x.RolesPermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_ApplicationPages_PageId",
                        column: x => x.PageId,
                        principalTable: "ApplicationPages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_CreatedById",
                table: "RolePermissions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_ModifiedById",
                table: "RolePermissions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PageId",
                table: "RolePermissions",
                column: "PageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions");
        }
    }
}

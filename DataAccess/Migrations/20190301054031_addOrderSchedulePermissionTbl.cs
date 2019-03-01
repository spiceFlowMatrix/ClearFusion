using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addOrderSchedulePermissionTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderSchedulePermission",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PageId = table.Column<int>(nullable: false),
                    OrderSchedule = table.Column<bool>(nullable: false),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSchedulePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSchedulePermission_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderSchedulePermission_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderSchedulePermission_ApplicationPages_PageId",
                        column: x => x.PageId,
                        principalTable: "ApplicationPages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderSchedulePermission_CreatedById",
                table: "OrderSchedulePermission",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSchedulePermission_ModifiedById",
                table: "OrderSchedulePermission",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSchedulePermission_PageId",
                table: "OrderSchedulePermission",
                column: "PageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderSchedulePermission");
        }
    }
}

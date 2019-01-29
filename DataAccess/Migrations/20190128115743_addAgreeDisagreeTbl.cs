using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addAgreeDisagreeTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgreeDisagreePermission",
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
                    Agree = table.Column<bool>(nullable: false),
                    Disagree = table.Column<bool>(nullable: false),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreeDisagreePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgreeDisagreePermission_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreeDisagreePermission_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreeDisagreePermission_ApplicationPages_PageId",
                        column: x => x.PageId,
                        principalTable: "ApplicationPages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreeDisagreePermission_CreatedById",
                table: "AgreeDisagreePermission",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AgreeDisagreePermission_ModifiedById",
                table: "AgreeDisagreePermission",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AgreeDisagreePermission_PageId",
                table: "AgreeDisagreePermission",
                column: "PageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgreeDisagreePermission");
        }
    }
}

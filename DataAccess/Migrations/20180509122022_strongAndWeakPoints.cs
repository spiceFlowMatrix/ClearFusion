using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class strongAndWeakPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StrongandWeakPoints",
                columns: table => new
                {
                    StrongPointsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CurrentAppraisalDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    EmployeeId = table.Column<int>(type: "int4", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Point = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrongandWeakPoints", x => x.StrongPointsId);
                    table.ForeignKey(
                        name: "FK_StrongandWeakPoints_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StrongandWeakPoints_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StrongandWeakPoints_CreatedById",
                table: "StrongandWeakPoints",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StrongandWeakPoints_ModifiedById",
                table: "StrongandWeakPoints",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StrongandWeakPoints");
        }
    }
}

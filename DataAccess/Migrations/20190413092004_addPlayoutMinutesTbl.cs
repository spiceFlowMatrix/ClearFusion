using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addPlayoutMinutesTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayoutMinutes",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PlayoutMinuteId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PolicyId = table.Column<long>(nullable: true),
                    TotalMinutes = table.Column<long>(nullable: true),
                    DroppedMinutes = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayoutMinutes", x => x.PlayoutMinuteId);
                    table.ForeignKey(
                        name: "FK_PlayoutMinutes_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayoutMinutes_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayoutMinutes_PolicyDetails_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "PolicyDetails",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayoutMinutes_CreatedById",
                table: "PlayoutMinutes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PlayoutMinutes_ModifiedById",
                table: "PlayoutMinutes",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PlayoutMinutes_PolicyId",
                table: "PlayoutMinutes",
                column: "PolicyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayoutMinutes");
        }
    }
}

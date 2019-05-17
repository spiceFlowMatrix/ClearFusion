using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class PensionDebitAccountMasterTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PensionDebitAccountMaster",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PensionDebitAccountId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ChartOfAccountNewId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PensionDebitAccountMaster", x => x.PensionDebitAccountId);
                    table.ForeignKey(
                        name: "FK_PensionDebitAccountMaster_ChartOfAccountNew_ChartOfAccountNewId",
                        column: x => x.ChartOfAccountNewId,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PensionDebitAccountMaster_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PensionDebitAccountMaster_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PensionDebitAccountMaster_ChartOfAccountNewId",
                table: "PensionDebitAccountMaster",
                column: "ChartOfAccountNewId");

            migrationBuilder.CreateIndex(
                name: "IX_PensionDebitAccountMaster_CreatedById",
                table: "PensionDebitAccountMaster",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PensionDebitAccountMaster_ModifiedById",
                table: "PensionDebitAccountMaster",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PensionDebitAccountMaster");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class GainLossSelectedAccountsTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GainLossSelectedAccounts",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    GainLossSelectedAccountId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ChartOfAccountNewId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GainLossSelectedAccounts", x => x.GainLossSelectedAccountId);
                    table.ForeignKey(
                        name: "FK_GainLossSelectedAccounts_ChartOfAccountNew_ChartOfAccountNewId",
                        column: x => x.ChartOfAccountNewId,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GainLossSelectedAccounts_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GainLossSelectedAccounts_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_ChartOfAccountNewId",
                table: "GainLossSelectedAccounts",
                column: "ChartOfAccountNewId");

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_CreatedById",
                table: "GainLossSelectedAccounts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_ModifiedById",
                table: "GainLossSelectedAccounts",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GainLossSelectedAccounts");
        }
    }
}

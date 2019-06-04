using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addInvoiceGenerationTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceGeneration",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    InvoiceId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    JobId = table.Column<long>(nullable: true),
                    PlayoutMinutes = table.Column<long>(nullable: true),
                    TotalMinutes = table.Column<long>(nullable: true),
                    TotalPrice = table.Column<double>(nullable: true),
                    JobPrice = table.Column<long>(nullable: true),
                    CurrencyId = table.Column<long>(nullable: true),
                    CurrencyDetailsCurrencyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceGeneration", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_InvoiceGeneration_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceGeneration_CurrencyDetails_CurrencyDetailsCurrencyId",
                        column: x => x.CurrencyDetailsCurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceGeneration_JobDetails_JobId",
                        column: x => x.JobId,
                        principalTable: "JobDetails",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceGeneration_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceGeneration_CreatedById",
                table: "InvoiceGeneration",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceGeneration_CurrencyDetailsCurrencyId",
                table: "InvoiceGeneration",
                column: "CurrencyDetailsCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceGeneration_JobId",
                table: "InvoiceGeneration",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceGeneration_ModifiedById",
                table: "InvoiceGeneration",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceGeneration");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class SalaryTaxContentModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalaryTaxReportContent",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SalaryTaxReportContentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployerAuthorizedOfficerName = table.Column<int>(nullable: false),
                    PositionAuthorizedOfficer = table.Column<int>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryTaxReportContent", x => x.SalaryTaxReportContentId);
                    table.ForeignKey(
                        name: "FK_SalaryTaxReportContent_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalaryTaxReportContent_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryTaxReportContent_CreatedById",
                table: "SalaryTaxReportContent",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryTaxReportContent_ModifiedById",
                table: "SalaryTaxReportContent",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryTaxReportContent");
        }
    }
}

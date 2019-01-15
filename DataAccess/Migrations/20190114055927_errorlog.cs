using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class errorlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "errorlog",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ExceptionId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Status = table.Column<int>(nullable: true),
                    stackTrace = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Section = table.Column<int>(nullable: true),
                    ModuleName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    DataXml = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_errorlog", x => x.ExceptionId);
                    table.ForeignKey(
                        name: "FK_errorlog_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_errorlog_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_errorlog_CreatedById",
                table: "errorlog",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_errorlog_ModifiedById",
                table: "errorlog",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "errorlog");
        }
    }
}

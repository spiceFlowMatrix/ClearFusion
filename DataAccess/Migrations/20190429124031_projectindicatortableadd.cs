using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class projectindicatortableadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectIndicators",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectIndicatorId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IndicatorName = table.Column<string>(nullable: true),
                    IndicatorCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectIndicators", x => x.ProjectIndicatorId);
                    table.ForeignKey(
                        name: "FK_ProjectIndicators_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectIndicators_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectIndicatorQuestions",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    IndicatorQuestionId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IndicatorQuestion = table.Column<string>(nullable: true),
                    ProjectIndicatorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectIndicatorQuestions", x => x.IndicatorQuestionId);
                    table.ForeignKey(
                        name: "FK_ProjectIndicatorQuestions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectIndicatorQuestions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectIndicatorQuestions_ProjectIndicators_ProjectIndicatorId",
                        column: x => x.ProjectIndicatorId,
                        principalTable: "ProjectIndicators",
                        principalColumn: "ProjectIndicatorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIndicatorQuestions_CreatedById",
                table: "ProjectIndicatorQuestions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIndicatorQuestions_ModifiedById",
                table: "ProjectIndicatorQuestions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIndicatorQuestions_ProjectIndicatorId",
                table: "ProjectIndicatorQuestions",
                column: "ProjectIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIndicators_CreatedById",
                table: "ProjectIndicators",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIndicators_ModifiedById",
                table: "ProjectIndicators",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectIndicatorQuestions");

            migrationBuilder.DropTable(
                name: "ProjectIndicators");
        }
    }
}

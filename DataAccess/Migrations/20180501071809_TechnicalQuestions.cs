using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class TechnicalQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterviewTechnicalQuestions",
                columns: table => new
                {
                    InterviewTechnicalQuestionsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    OfficeId = table.Column<int>(type: "int4", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewTechnicalQuestions", x => x.InterviewTechnicalQuestionsId);
                    table.ForeignKey(
                        name: "FK_InterviewTechnicalQuestions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewTechnicalQuestions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestions_CreatedById",
                table: "InterviewTechnicalQuestions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestions_ModifiedById",
                table: "InterviewTechnicalQuestions",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterviewTechnicalQuestions");
        }
    }
}

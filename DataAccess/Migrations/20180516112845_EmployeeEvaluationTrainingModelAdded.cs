using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class EmployeeEvaluationTrainingModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatchLevel",
                table: "EmployeeEvaluation");

            migrationBuilder.DropColumn(
                name: "OthRecommendation",
                table: "EmployeeEvaluation");

            migrationBuilder.DropColumn(
                name: "Participated",
                table: "EmployeeEvaluation");

            migrationBuilder.DropColumn(
                name: "Program",
                table: "EmployeeEvaluation");

            migrationBuilder.DropColumn(
                name: "RefresherTrm",
                table: "EmployeeEvaluation");

            migrationBuilder.DropColumn(
                name: "StrongPoints",
                table: "EmployeeEvaluation");

            migrationBuilder.DropColumn(
                name: "TrainingProgram",
                table: "EmployeeEvaluation");

            migrationBuilder.DropColumn(
                name: "WeakPoints",
                table: "EmployeeEvaluation");

            migrationBuilder.CreateTable(
                name: "EmployeeEvaluationTraining",
                columns: table => new
                {
                    EmployeeEvaluationTrainingId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CatchLevel = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    EmployeeAppraisalDetailsId = table.Column<int>(type: "int4", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    OthRecommendation = table.Column<string>(type: "text", nullable: true),
                    Participated = table.Column<string>(type: "text", nullable: true),
                    Program = table.Column<string>(type: "text", nullable: true),
                    RefresherTrm = table.Column<string>(type: "text", nullable: true),
                    TrainingProgram = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEvaluationTraining", x => x.EmployeeEvaluationTrainingId);
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluationTraining_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluationTraining_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluationTraining_CreatedById",
                table: "EmployeeEvaluationTraining",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluationTraining_ModifiedById",
                table: "EmployeeEvaluationTraining",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeEvaluationTraining");

            migrationBuilder.AddColumn<string>(
                name: "CatchLevel",
                table: "EmployeeEvaluation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OthRecommendation",
                table: "EmployeeEvaluation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Participated",
                table: "EmployeeEvaluation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Program",
                table: "EmployeeEvaluation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefresherTrm",
                table: "EmployeeEvaluation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrongPoints",
                table: "EmployeeEvaluation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingProgram",
                table: "EmployeeEvaluation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeakPoints",
                table: "EmployeeEvaluation",
                nullable: true);
        }
    }
}

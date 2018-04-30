using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class employeeEvaluation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeEvaluation",
                columns: table => new
                {
                    EmployeeEvaluationId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AppraisalTeamMember1 = table.Column<string>(type: "text", nullable: true),
                    AppraisalTeamMember2 = table.Column<string>(type: "text", nullable: true),
                    CatchLevel = table.Column<string>(type: "text", nullable: true),
                    CommentsByEmployee = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CurrentAppraisalDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DirectSupervisor = table.Column<string>(type: "text", nullable: true),
                    EmployeeId = table.Column<int>(type: "int4", nullable: false),
                    FinalResultQues1 = table.Column<string>(type: "text", nullable: true),
                    FinalResultQues2 = table.Column<string>(type: "text", nullable: true),
                    FinalResultQues3 = table.Column<string>(type: "text", nullable: true),
                    FinalResultQues4 = table.Column<string>(type: "text", nullable: true),
                    FinalResultQues5 = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    OthRecommendation = table.Column<string>(type: "text", nullable: true),
                    Participated = table.Column<string>(type: "text", nullable: true),
                    Program = table.Column<string>(type: "text", nullable: true),
                    RefresherTrm = table.Column<string>(type: "text", nullable: true),
                    StrongPoints = table.Column<string>(type: "text", nullable: true),
                    TrainingProgram = table.Column<string>(type: "text", nullable: true),
                    WeakPoints = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEvaluation", x => x.EmployeeEvaluationId);
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluation_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluation_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluation_CreatedById",
                table: "EmployeeEvaluation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluation_ModifiedById",
                table: "EmployeeEvaluation",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeEvaluation");
        }
    }
}

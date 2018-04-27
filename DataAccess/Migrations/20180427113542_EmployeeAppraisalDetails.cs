using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class EmployeeAppraisalDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeAppraisalDetails",
                columns: table => new
                {
                    EmployeeAppraisalDetailsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AppraisalPeriod = table.Column<int>(type: "int4", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CurrentAppraisalDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: true),
                    DutyStation = table.Column<string>(type: "text", nullable: true),
                    EmployeeCode = table.Column<string>(type: "text", nullable: true),
                    EmployeeId = table.Column<int>(type: "int4", nullable: false),
                    EmployeeName = table.Column<string>(type: "text", nullable: true),
                    FatherName = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Position = table.Column<string>(type: "text", nullable: true),
                    Qualification = table.Column<string>(type: "text", nullable: true),
                    RecruitmentDate = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAppraisalDetails", x => x.EmployeeAppraisalDetailsId);
                    table.ForeignKey(
                        name: "FK_EmployeeAppraisalDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeAppraisalDetails_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAppraisalDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAppraisalQuestions",
                columns: table => new
                {
                    EmployeeAppraisalQuestionsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AppraisalGeneralQuestionsId = table.Column<int>(type: "int4", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    Score = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAppraisalQuestions", x => x.EmployeeAppraisalQuestionsId);
                    table.ForeignKey(
                        name: "FK_EmployeeAppraisalQuestions_AppraisalGeneralQuestions_AppraisalGeneralQuestionsId",
                        column: x => x.AppraisalGeneralQuestionsId,
                        principalTable: "AppraisalGeneralQuestions",
                        principalColumn: "AppraisalGeneralQuestionsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAppraisalQuestions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeAppraisalQuestions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalDetails_CreatedById",
                table: "EmployeeAppraisalDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalDetails_EmployeeId",
                table: "EmployeeAppraisalDetails",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalDetails_ModifiedById",
                table: "EmployeeAppraisalDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalQuestions_AppraisalGeneralQuestionsId",
                table: "EmployeeAppraisalQuestions",
                column: "AppraisalGeneralQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalQuestions_CreatedById",
                table: "EmployeeAppraisalQuestions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalQuestions_ModifiedById",
                table: "EmployeeAppraisalQuestions",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeAppraisalDetails");

            migrationBuilder.DropTable(
                name: "EmployeeAppraisalQuestions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class interviewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterviewDetails",
                columns: table => new
                {
                    InterviewDetailsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CandidateName = table.Column<string>(type: "text", nullable: true),
                    CandidatePosition = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DutyStation = table.Column<string>(type: "text", nullable: true),
                    Experience = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<int>(type: "int4", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    MaritalStatus = table.Column<string>(type: "text", nullable: true),
                    MarksObtained = table.Column<string>(type: "text", nullable: true),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    NoticePeriod = table.Column<string>(type: "text", nullable: true),
                    PassportNo = table.Column<string>(type: "text", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "text", nullable: true),
                    PreferedLocation = table.Column<string>(type: "text", nullable: true),
                    ProfessionalCriteriaMarks = table.Column<string>(type: "text", nullable: true),
                    Qualification = table.Column<string>(type: "text", nullable: true),
                    Ques1 = table.Column<string>(type: "text", nullable: true),
                    Ques2 = table.Column<string>(type: "text", nullable: true),
                    Ques3 = table.Column<string>(type: "text", nullable: true),
                    ResidingProvince = table.Column<string>(type: "text", nullable: true),
                    TazkiraIssuePlace = table.Column<string>(type: "text", nullable: true),
                    University = table.Column<string>(type: "text", nullable: true),
                    WrittenTestMarks = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewDetails", x => x.InterviewDetailsId);
                    table.ForeignKey(
                        name: "FK_InterviewDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalQuestion",
                columns: table => new
                {
                    TechnicalQuestionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Question = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalQuestion", x => x.TechnicalQuestionId);
                    table.ForeignKey(
                        name: "FK_TechnicalQuestion_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TechnicalQuestion_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterviewLanguages",
                columns: table => new
                {
                    InterviewLanguagesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    InterviewDetailsId = table.Column<int>(type: "int4", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    LanguageId = table.Column<int>(type: "int4", nullable: false),
                    LanguageName = table.Column<string>(type: "text", nullable: true),
                    Listening = table.Column<int>(type: "int4", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Reading = table.Column<int>(type: "int4", nullable: false),
                    Speaking = table.Column<int>(type: "int4", nullable: false),
                    Writing = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewLanguages", x => x.InterviewLanguagesId);
                    table.ForeignKey(
                        name: "FK_InterviewLanguages_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewLanguages_InterviewDetails_InterviewDetailsId",
                        column: x => x.InterviewDetailsId,
                        principalTable: "InterviewDetails",
                        principalColumn: "InterviewDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewLanguages_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterviewTrainings",
                columns: table => new
                {
                    InterviewTrainingsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    InterviewDetailsId = table.Column<int>(type: "int4", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    StudyingCountry = table.Column<string>(type: "text", nullable: true),
                    TrainingName = table.Column<string>(type: "text", nullable: true),
                    TraininigType = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewTrainings", x => x.InterviewTrainingsId);
                    table.ForeignKey(
                        name: "FK_InterviewTrainings_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewTrainings_InterviewDetails_InterviewDetailsId",
                        column: x => x.InterviewDetailsId,
                        principalTable: "InterviewDetails",
                        principalColumn: "InterviewDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewTrainings_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterviewTechnicalQuestion",
                columns: table => new
                {
                    InterviewTechnicalQuestionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Excellent = table.Column<int>(type: "int4", nullable: false),
                    Fair = table.Column<int>(type: "int4", nullable: false),
                    Good = table.Column<int>(type: "int4", nullable: false),
                    InterviewDetailsId = table.Column<int>(type: "int4", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Perfect = table.Column<int>(type: "int4", nullable: false),
                    Poor = table.Column<int>(type: "int4", nullable: false),
                    TechnicalQuestionId = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewTechnicalQuestion", x => x.InterviewTechnicalQuestionId);
                    table.ForeignKey(
                        name: "FK_InterviewTechnicalQuestion_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewTechnicalQuestion_InterviewDetails_InterviewDetailsId",
                        column: x => x.InterviewDetailsId,
                        principalTable: "InterviewDetails",
                        principalColumn: "InterviewDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewTechnicalQuestion_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewTechnicalQuestion_TechnicalQuestion_TechnicalQuestionId",
                        column: x => x.TechnicalQuestionId,
                        principalTable: "TechnicalQuestion",
                        principalColumn: "TechnicalQuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterviewDetails_CreatedById",
                table: "InterviewDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewDetails_ModifiedById",
                table: "InterviewDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewLanguages_CreatedById",
                table: "InterviewLanguages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewLanguages_InterviewDetailsId",
                table: "InterviewLanguages",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewLanguages_ModifiedById",
                table: "InterviewLanguages",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestion_CreatedById",
                table: "InterviewTechnicalQuestion",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestion_InterviewDetailsId",
                table: "InterviewTechnicalQuestion",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestion_ModifiedById",
                table: "InterviewTechnicalQuestion",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestion_TechnicalQuestionId",
                table: "InterviewTechnicalQuestion",
                column: "TechnicalQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTrainings_CreatedById",
                table: "InterviewTrainings",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTrainings_InterviewDetailsId",
                table: "InterviewTrainings",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTrainings_ModifiedById",
                table: "InterviewTrainings",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalQuestion_CreatedById",
                table: "TechnicalQuestion",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalQuestion_ModifiedById",
                table: "TechnicalQuestion",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterviewLanguages");

            migrationBuilder.DropTable(
                name: "InterviewTechnicalQuestion");

            migrationBuilder.DropTable(
                name: "InterviewTrainings");

            migrationBuilder.DropTable(
                name: "TechnicalQuestion");

            migrationBuilder.DropTable(
                name: "InterviewDetails");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class exitInterviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExistInterviewDetails",
                columns: table => new
                {
                    ExistInterviewDetailsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BenefitProgram = table.Column<string>(type: "text", nullable: true),
                    Benefits = table.Column<bool>(type: "bool", nullable: false),
                    BetterJobOpportunity = table.Column<bool>(type: "bool", nullable: false),
                    CareerChange = table.Column<bool>(type: "bool", nullable: false),
                    CoWorkers = table.Column<string>(type: "text", nullable: true),
                    ComfortableAppropriately = table.Column<string>(type: "text", nullable: true),
                    CompanyInstability = table.Column<bool>(type: "bool", nullable: false),
                    ConflictWithOther = table.Column<bool>(type: "bool", nullable: false),
                    ConflictWithSuoervisors = table.Column<bool>(type: "bool", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DutiesOfJob = table.Column<string>(type: "text", nullable: true),
                    EmployeeCode = table.Column<string>(type: "text", nullable: true),
                    EmployeeId = table.Column<int>(type: "int4", nullable: false),
                    EncouragedCooperation = table.Column<string>(type: "text", nullable: true),
                    Equipped = table.Column<string>(type: "text", nullable: true),
                    Explain = table.Column<string>(type: "text", nullable: true),
                    FamilyReasons = table.Column<bool>(type: "bool", nullable: false),
                    GaveFairTreatment = table.Column<string>(type: "text", nullable: true),
                    GenderFriendlyEnvironment = table.Column<string>(type: "text", nullable: true),
                    HadAdequateEquipment = table.Column<string>(type: "text", nullable: true),
                    HadGoodSynergy = table.Column<string>(type: "text", nullable: true),
                    HadKnowledgeOfJob = table.Column<string>(type: "text", nullable: true),
                    HadKnowledgeSupervision = table.Column<string>(type: "text", nullable: true),
                    HealthIssue = table.Column<bool>(type: "bool", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    JobOrientation = table.Column<string>(type: "text", nullable: true),
                    JobWasChallenging = table.Column<string>(type: "text", nullable: true),
                    MaintainedConsistent = table.Column<string>(type: "text", nullable: true),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    NotChallenged = table.Column<bool>(type: "bool", nullable: false),
                    OpportunityAdvancement = table.Column<string>(type: "text", nullable: true),
                    OverallJobSatisfaction = table.Column<string>(type: "text", nullable: true),
                    Pay = table.Column<bool>(type: "bool", nullable: false),
                    PersonalReasons = table.Column<bool>(type: "bool", nullable: false),
                    ProvidedDevelopment = table.Column<string>(type: "text", nullable: true),
                    ProvidedRecognition = table.Column<string>(type: "text", nullable: true),
                    Question = table.Column<bool>(type: "bool", nullable: false),
                    RecognizedEmployeesContribution = table.Column<string>(type: "text", nullable: true),
                    Relocation = table.Column<bool>(type: "bool", nullable: false),
                    ReturnToSchool = table.Column<bool>(type: "bool", nullable: false),
                    SalaryTreatment = table.Column<string>(type: "text", nullable: true),
                    SkillsEffectivelyUsed = table.Column<string>(type: "text", nullable: true),
                    SufficientResources = table.Column<string>(type: "text", nullable: true),
                    Supervisors = table.Column<string>(type: "text", nullable: true),
                    TrainingAndDevelopmentPrograms = table.Column<string>(type: "text", nullable: true),
                    WasAdequatelyStaffed = table.Column<string>(type: "text", nullable: true),
                    WasAvailableToDiscuss = table.Column<string>(type: "text", nullable: true),
                    WasEfficient = table.Column<string>(type: "text", nullable: true),
                    WasOpenSuggestions = table.Column<string>(type: "text", nullable: true),
                    WelcomedSuggestions = table.Column<string>(type: "text", nullable: true),
                    WorkEnvironment = table.Column<string>(type: "text", nullable: true),
                    WorkLoadReasonable = table.Column<string>(type: "text", nullable: true),
                    WorkRelationship = table.Column<bool>(type: "bool", nullable: false),
                    WorkingConditions = table.Column<string>(type: "text", nullable: true),
                    WorkingHours = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExistInterviewDetails", x => x.ExistInterviewDetailsId);
                    table.ForeignKey(
                        name: "FK_ExistInterviewDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExistInterviewDetails_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExistInterviewDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExistInterviewDetails_CreatedById",
                table: "ExistInterviewDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ExistInterviewDetails_EmployeeId",
                table: "ExistInterviewDetails",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExistInterviewDetails_ModifiedById",
                table: "ExistInterviewDetails",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExistInterviewDetails");
        }
    }
}

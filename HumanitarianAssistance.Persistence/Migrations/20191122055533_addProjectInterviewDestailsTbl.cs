using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addProjectInterviewDestailsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HRJobInterviewers_InterviewDetails_InterviewDetailsId",
                table: "HRJobInterviewers");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewLanguages_InterviewDetails_InterviewDetailsId",
                table: "InterviewLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewTechnicalQuestion_InterviewDetails_InterviewDetail~",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewTrainings_InterviewDetails_InterviewDetailsId",
                table: "InterviewTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingBasedCriteria_InterviewDetails_InterviewDetailsId",
                table: "RatingBasedCriteria");

            migrationBuilder.AlterColumn<int>(
                name: "InterviewDetailsId",
                table: "RatingBasedCriteria",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProjectInterviewDetailsInterviewId",
                table: "RatingBasedCriteria",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "RatingBasedCriteria",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "RatingBasedCriteria",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TraininigType",
                table: "InterviewTrainings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "InterviewDetailsId",
                table: "InterviewTrainings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "NewTraininigType",
                table: "InterviewTrainings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectInterviewDetailsInterviewId",
                table: "InterviewTrainings",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InterviewDetailsId",
                table: "InterviewTechnicalQuestion",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProjectInterviewDetailsInterviewId",
                table: "InterviewTechnicalQuestion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "InterviewTechnicalQuestion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "InterviewTechnicalQuestion",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InterviewDetailsId",
                table: "InterviewLanguages",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProjectInterviewDetailsInterviewId",
                table: "InterviewLanguages",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InterviewDetailsId",
                table: "HRJobInterviewers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProjectInterviewDetailsInterviewId",
                table: "HRJobInterviewers",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InterviewId",
                table: "HiringRequestCandidateStatus",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "ProjectInterviewDetails",
                columns: table => new
                {
                    InterviewId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    NoticePeriod = table.Column<int>(nullable: false),
                    AvailableDate = table.Column<DateTime>(nullable: true),
                    WrittenTestMarks = table.Column<int>(nullable: false),
                    CurrentBase = table.Column<int>(nullable: false),
                    CurrentTransport = table.Column<bool>(nullable: false),
                    CurrentMeal = table.Column<bool>(nullable: false),
                    CurrentOther = table.Column<int>(nullable: false),
                    ExpectationBase = table.Column<int>(nullable: false),
                    ExpectationTransport = table.Column<bool>(nullable: false),
                    ExpectationMeal = table.Column<bool>(nullable: false),
                    ExpectationOther = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    InterviewQuestionOne = table.Column<bool>(nullable: false),
                    InterviewQuestionTwo = table.Column<bool>(nullable: false),
                    InterviewQuestionThree = table.Column<bool>(nullable: false),
                    ProfessionalCriteriaMarks = table.Column<double>(nullable: false),
                    MarksObtained = table.Column<int>(nullable: false),
                    TotalMarksObtained = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectInterviewDetails", x => x.InterviewId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatingBasedCriteria_ProjectInterviewDetailsInterviewId",
                table: "RatingBasedCriteria",
                column: "ProjectInterviewDetailsInterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingBasedCriteria_QuestionId",
                table: "RatingBasedCriteria",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTrainings_ProjectInterviewDetailsInterviewId",
                table: "InterviewTrainings",
                column: "ProjectInterviewDetailsInterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestion_ProjectInterviewDetailsInterview~",
                table: "InterviewTechnicalQuestion",
                column: "ProjectInterviewDetailsInterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestion_QuestionId",
                table: "InterviewTechnicalQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewLanguages_ProjectInterviewDetailsInterviewId",
                table: "InterviewLanguages",
                column: "ProjectInterviewDetailsInterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_HRJobInterviewers_ProjectInterviewDetailsInterviewId",
                table: "HRJobInterviewers",
                column: "ProjectInterviewDetailsInterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidateStatus_InterviewId",
                table: "HiringRequestCandidateStatus",
                column: "InterviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_HiringRequestCandidateStatus_ProjectInterviewDetails_Interv~",
                table: "HiringRequestCandidateStatus",
                column: "InterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HRJobInterviewers_InterviewDetails_InterviewDetailsId",
                table: "HRJobInterviewers",
                column: "InterviewDetailsId",
                principalTable: "InterviewDetails",
                principalColumn: "InterviewDetailsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HRJobInterviewers_ProjectInterviewDetails_ProjectInterviewD~",
                table: "HRJobInterviewers",
                column: "ProjectInterviewDetailsInterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewLanguages_InterviewDetails_InterviewDetailsId",
                table: "InterviewLanguages",
                column: "InterviewDetailsId",
                principalTable: "InterviewDetails",
                principalColumn: "InterviewDetailsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewLanguages_ProjectInterviewDetails_ProjectInterview~",
                table: "InterviewLanguages",
                column: "ProjectInterviewDetailsInterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewTechnicalQuestion_InterviewDetails_InterviewDetail~",
                table: "InterviewTechnicalQuestion",
                column: "InterviewDetailsId",
                principalTable: "InterviewDetails",
                principalColumn: "InterviewDetailsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewTechnicalQuestion_ProjectInterviewDetails_ProjectI~",
                table: "InterviewTechnicalQuestion",
                column: "ProjectInterviewDetailsInterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewTechnicalQuestion_TechnicalQuestion_QuestionId",
                table: "InterviewTechnicalQuestion",
                column: "QuestionId",
                principalTable: "TechnicalQuestion",
                principalColumn: "TechnicalQuestionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewTrainings_InterviewDetails_InterviewDetailsId",
                table: "InterviewTrainings",
                column: "InterviewDetailsId",
                principalTable: "InterviewDetails",
                principalColumn: "InterviewDetailsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewTrainings_ProjectInterviewDetails_ProjectInterview~",
                table: "InterviewTrainings",
                column: "ProjectInterviewDetailsInterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingBasedCriteria_InterviewDetails_InterviewDetailsId",
                table: "RatingBasedCriteria",
                column: "InterviewDetailsId",
                principalTable: "InterviewDetails",
                principalColumn: "InterviewDetailsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingBasedCriteria_ProjectInterviewDetails_ProjectIntervie~",
                table: "RatingBasedCriteria",
                column: "ProjectInterviewDetailsInterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingBasedCriteria_RatingBasedCriteriaQuestions_QuestionId",
                table: "RatingBasedCriteria",
                column: "QuestionId",
                principalTable: "RatingBasedCriteriaQuestions",
                principalColumn: "QuestionsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HiringRequestCandidateStatus_ProjectInterviewDetails_Interv~",
                table: "HiringRequestCandidateStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_HRJobInterviewers_InterviewDetails_InterviewDetailsId",
                table: "HRJobInterviewers");

            migrationBuilder.DropForeignKey(
                name: "FK_HRJobInterviewers_ProjectInterviewDetails_ProjectInterviewD~",
                table: "HRJobInterviewers");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewLanguages_InterviewDetails_InterviewDetailsId",
                table: "InterviewLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewLanguages_ProjectInterviewDetails_ProjectInterview~",
                table: "InterviewLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewTechnicalQuestion_InterviewDetails_InterviewDetail~",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewTechnicalQuestion_ProjectInterviewDetails_ProjectI~",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewTechnicalQuestion_TechnicalQuestion_QuestionId",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewTrainings_InterviewDetails_InterviewDetailsId",
                table: "InterviewTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewTrainings_ProjectInterviewDetails_ProjectInterview~",
                table: "InterviewTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingBasedCriteria_InterviewDetails_InterviewDetailsId",
                table: "RatingBasedCriteria");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingBasedCriteria_ProjectInterviewDetails_ProjectIntervie~",
                table: "RatingBasedCriteria");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingBasedCriteria_RatingBasedCriteriaQuestions_QuestionId",
                table: "RatingBasedCriteria");

            migrationBuilder.DropTable(
                name: "ProjectInterviewDetails");

            migrationBuilder.DropIndex(
                name: "IX_RatingBasedCriteria_ProjectInterviewDetailsInterviewId",
                table: "RatingBasedCriteria");

            migrationBuilder.DropIndex(
                name: "IX_RatingBasedCriteria_QuestionId",
                table: "RatingBasedCriteria");

            migrationBuilder.DropIndex(
                name: "IX_InterviewTrainings_ProjectInterviewDetailsInterviewId",
                table: "InterviewTrainings");

            migrationBuilder.DropIndex(
                name: "IX_InterviewTechnicalQuestion_ProjectInterviewDetailsInterview~",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropIndex(
                name: "IX_InterviewTechnicalQuestion_QuestionId",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropIndex(
                name: "IX_InterviewLanguages_ProjectInterviewDetailsInterviewId",
                table: "InterviewLanguages");

            migrationBuilder.DropIndex(
                name: "IX_HRJobInterviewers_ProjectInterviewDetailsInterviewId",
                table: "HRJobInterviewers");

            migrationBuilder.DropIndex(
                name: "IX_HiringRequestCandidateStatus_InterviewId",
                table: "HiringRequestCandidateStatus");

            migrationBuilder.DropColumn(
                name: "ProjectInterviewDetailsInterviewId",
                table: "RatingBasedCriteria");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "RatingBasedCriteria");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "RatingBasedCriteria");

            migrationBuilder.DropColumn(
                name: "NewTraininigType",
                table: "InterviewTrainings");

            migrationBuilder.DropColumn(
                name: "ProjectInterviewDetailsInterviewId",
                table: "InterviewTrainings");

            migrationBuilder.DropColumn(
                name: "ProjectInterviewDetailsInterviewId",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropColumn(
                name: "ProjectInterviewDetailsInterviewId",
                table: "InterviewLanguages");

            migrationBuilder.DropColumn(
                name: "ProjectInterviewDetailsInterviewId",
                table: "HRJobInterviewers");

            migrationBuilder.AlterColumn<int>(
                name: "InterviewDetailsId",
                table: "RatingBasedCriteria",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TraininigType",
                table: "InterviewTrainings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InterviewDetailsId",
                table: "InterviewTrainings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InterviewDetailsId",
                table: "InterviewTechnicalQuestion",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InterviewDetailsId",
                table: "InterviewLanguages",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InterviewDetailsId",
                table: "HRJobInterviewers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InterviewId",
                table: "HiringRequestCandidateStatus",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HRJobInterviewers_InterviewDetails_InterviewDetailsId",
                table: "HRJobInterviewers",
                column: "InterviewDetailsId",
                principalTable: "InterviewDetails",
                principalColumn: "InterviewDetailsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewLanguages_InterviewDetails_InterviewDetailsId",
                table: "InterviewLanguages",
                column: "InterviewDetailsId",
                principalTable: "InterviewDetails",
                principalColumn: "InterviewDetailsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewTechnicalQuestion_InterviewDetails_InterviewDetail~",
                table: "InterviewTechnicalQuestion",
                column: "InterviewDetailsId",
                principalTable: "InterviewDetails",
                principalColumn: "InterviewDetailsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewTrainings_InterviewDetails_InterviewDetailsId",
                table: "InterviewTrainings",
                column: "InterviewDetailsId",
                principalTable: "InterviewDetails",
                principalColumn: "InterviewDetailsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingBasedCriteria_InterviewDetails_InterviewDetailsId",
                table: "RatingBasedCriteria",
                column: "InterviewDetailsId",
                principalTable: "InterviewDetails",
                principalColumn: "InterviewDetailsId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

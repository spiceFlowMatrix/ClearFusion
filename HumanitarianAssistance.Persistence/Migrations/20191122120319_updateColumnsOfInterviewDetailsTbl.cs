using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateColumnsOfInterviewDetailsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HRJobInterviewers_ProjectInterviewDetails_ProjectInterviewD~",
                table: "HRJobInterviewers");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewLanguages_ProjectInterviewDetails_ProjectInterview~",
                table: "InterviewLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewTechnicalQuestion_ProjectInterviewDetails_ProjectI~",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewTrainings_ProjectInterviewDetails_ProjectInterview~",
                table: "InterviewTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingBasedCriteria_ProjectInterviewDetails_ProjectIntervie~",
                table: "RatingBasedCriteria");

            migrationBuilder.RenameColumn(
                name: "ProjectInterviewDetailsInterviewId",
                table: "RatingBasedCriteria",
                newName: "InterviewId");

            migrationBuilder.RenameIndex(
                name: "IX_RatingBasedCriteria_ProjectInterviewDetailsInterviewId",
                table: "RatingBasedCriteria",
                newName: "IX_RatingBasedCriteria_InterviewId");

            migrationBuilder.RenameColumn(
                name: "ProjectInterviewDetailsInterviewId",
                table: "InterviewTrainings",
                newName: "InterviewId");

            migrationBuilder.RenameIndex(
                name: "IX_InterviewTrainings_ProjectInterviewDetailsInterviewId",
                table: "InterviewTrainings",
                newName: "IX_InterviewTrainings_InterviewId");

            migrationBuilder.RenameColumn(
                name: "ProjectInterviewDetailsInterviewId",
                table: "InterviewTechnicalQuestion",
                newName: "InterviewId");

            migrationBuilder.RenameIndex(
                name: "IX_InterviewTechnicalQuestion_ProjectInterviewDetailsInterview~",
                table: "InterviewTechnicalQuestion",
                newName: "IX_InterviewTechnicalQuestion_InterviewId");

            migrationBuilder.RenameColumn(
                name: "ProjectInterviewDetailsInterviewId",
                table: "InterviewLanguages",
                newName: "InterviewId");

            migrationBuilder.RenameIndex(
                name: "IX_InterviewLanguages_ProjectInterviewDetailsInterviewId",
                table: "InterviewLanguages",
                newName: "IX_InterviewLanguages_InterviewId");

            migrationBuilder.RenameColumn(
                name: "ProjectInterviewDetailsInterviewId",
                table: "HRJobInterviewers",
                newName: "InterviewId");

            migrationBuilder.RenameIndex(
                name: "IX_HRJobInterviewers_ProjectInterviewDetailsInterviewId",
                table: "HRJobInterviewers",
                newName: "IX_HRJobInterviewers_InterviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_HRJobInterviewers_ProjectInterviewDetails_InterviewId",
                table: "HRJobInterviewers",
                column: "InterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewLanguages_ProjectInterviewDetails_InterviewId",
                table: "InterviewLanguages",
                column: "InterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewTechnicalQuestion_ProjectInterviewDetails_Intervie~",
                table: "InterviewTechnicalQuestion",
                column: "InterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewTrainings_ProjectInterviewDetails_InterviewId",
                table: "InterviewTrainings",
                column: "InterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingBasedCriteria_ProjectInterviewDetails_InterviewId",
                table: "RatingBasedCriteria",
                column: "InterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HRJobInterviewers_ProjectInterviewDetails_InterviewId",
                table: "HRJobInterviewers");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewLanguages_ProjectInterviewDetails_InterviewId",
                table: "InterviewLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewTechnicalQuestion_ProjectInterviewDetails_Intervie~",
                table: "InterviewTechnicalQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewTrainings_ProjectInterviewDetails_InterviewId",
                table: "InterviewTrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingBasedCriteria_ProjectInterviewDetails_InterviewId",
                table: "RatingBasedCriteria");

            migrationBuilder.RenameColumn(
                name: "InterviewId",
                table: "RatingBasedCriteria",
                newName: "ProjectInterviewDetailsInterviewId");

            migrationBuilder.RenameIndex(
                name: "IX_RatingBasedCriteria_InterviewId",
                table: "RatingBasedCriteria",
                newName: "IX_RatingBasedCriteria_ProjectInterviewDetailsInterviewId");

            migrationBuilder.RenameColumn(
                name: "InterviewId",
                table: "InterviewTrainings",
                newName: "ProjectInterviewDetailsInterviewId");

            migrationBuilder.RenameIndex(
                name: "IX_InterviewTrainings_InterviewId",
                table: "InterviewTrainings",
                newName: "IX_InterviewTrainings_ProjectInterviewDetailsInterviewId");

            migrationBuilder.RenameColumn(
                name: "InterviewId",
                table: "InterviewTechnicalQuestion",
                newName: "ProjectInterviewDetailsInterviewId");

            migrationBuilder.RenameIndex(
                name: "IX_InterviewTechnicalQuestion_InterviewId",
                table: "InterviewTechnicalQuestion",
                newName: "IX_InterviewTechnicalQuestion_ProjectInterviewDetailsInterview~");

            migrationBuilder.RenameColumn(
                name: "InterviewId",
                table: "InterviewLanguages",
                newName: "ProjectInterviewDetailsInterviewId");

            migrationBuilder.RenameIndex(
                name: "IX_InterviewLanguages_InterviewId",
                table: "InterviewLanguages",
                newName: "IX_InterviewLanguages_ProjectInterviewDetailsInterviewId");

            migrationBuilder.RenameColumn(
                name: "InterviewId",
                table: "HRJobInterviewers",
                newName: "ProjectInterviewDetailsInterviewId");

            migrationBuilder.RenameIndex(
                name: "IX_HRJobInterviewers_InterviewId",
                table: "HRJobInterviewers",
                newName: "IX_HRJobInterviewers_ProjectInterviewDetailsInterviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_HRJobInterviewers_ProjectInterviewDetails_ProjectInterviewD~",
                table: "HRJobInterviewers",
                column: "ProjectInterviewDetailsInterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewLanguages_ProjectInterviewDetails_ProjectInterview~",
                table: "InterviewLanguages",
                column: "ProjectInterviewDetailsInterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewTechnicalQuestion_ProjectInterviewDetails_ProjectI~",
                table: "InterviewTechnicalQuestion",
                column: "ProjectInterviewDetailsInterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewTrainings_ProjectInterviewDetails_ProjectInterview~",
                table: "InterviewTrainings",
                column: "ProjectInterviewDetailsInterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingBasedCriteria_ProjectInterviewDetails_ProjectIntervie~",
                table: "RatingBasedCriteria",
                column: "ProjectInterviewDetailsInterviewId",
                principalTable: "ProjectInterviewDetails",
                principalColumn: "InterviewId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

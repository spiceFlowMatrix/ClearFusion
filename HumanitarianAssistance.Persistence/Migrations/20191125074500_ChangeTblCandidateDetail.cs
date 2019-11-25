using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class ChangeTblCandidateDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateDetails_EducationDegreeDetails_EducationDegreeId",
                table: "CandidateDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateDetails_JobGrade_GradeId",
                table: "CandidateDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateDetails_OfficeDetail_OfficeId",
                table: "CandidateDetails");

            migrationBuilder.DropIndex(
                name: "IX_CandidateDetails_GradeId",
                table: "CandidateDetails");

            migrationBuilder.DropIndex(
                name: "IX_CandidateDetails_OfficeId",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "AccountStatus",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "TotalExperienceInYear",
                table: "CandidateDetails");

            migrationBuilder.RenameColumn(
                name: "OfficeId",
                table: "CandidateDetails",
                newName: "ExperienceYear");

            migrationBuilder.RenameColumn(
                name: "GradeId",
                table: "CandidateDetails",
                newName: "ExperienceMonth");

            migrationBuilder.AlterColumn<int>(
                name: "EducationDegreeId",
                table: "CandidateDetails",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "CurrentAddress",
                table: "CandidateDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentAddress",
                table: "CandidateDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousWork",
                table: "CandidateDetails",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateDetails_EducationDegreeMaster_EducationDegreeId",
                table: "CandidateDetails",
                column: "EducationDegreeId",
                principalTable: "EducationDegreeMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateDetails_EducationDegreeMaster_EducationDegreeId",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "CurrentAddress",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "PermanentAddress",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "PreviousWork",
                table: "CandidateDetails");

            migrationBuilder.RenameColumn(
                name: "ExperienceYear",
                table: "CandidateDetails",
                newName: "OfficeId");

            migrationBuilder.RenameColumn(
                name: "ExperienceMonth",
                table: "CandidateDetails",
                newName: "GradeId");

            migrationBuilder.AlterColumn<long>(
                name: "EducationDegreeId",
                table: "CandidateDetails",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AccountStatus",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TotalExperienceInYear",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_GradeId",
                table: "CandidateDetails",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_OfficeId",
                table: "CandidateDetails",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateDetails_EducationDegreeDetails_EducationDegreeId",
                table: "CandidateDetails",
                column: "EducationDegreeId",
                principalTable: "EducationDegreeDetails",
                principalColumn: "EducationDegreeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateDetails_JobGrade_GradeId",
                table: "CandidateDetails",
                column: "GradeId",
                principalTable: "JobGrade",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateDetails_OfficeDetail_OfficeId",
                table: "CandidateDetails",
                column: "OfficeId",
                principalTable: "OfficeDetail",
                principalColumn: "OfficeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

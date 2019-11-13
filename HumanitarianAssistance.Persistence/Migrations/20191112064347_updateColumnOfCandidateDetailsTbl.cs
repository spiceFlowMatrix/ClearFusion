using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateColumnOfCandidateDetailsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EducationDegreeId",
                table: "CandidateDetails",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_EducationDegreeId",
                table: "CandidateDetails",
                column: "EducationDegreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateDetails_EducationDegreeDetails_EducationDegreeId",
                table: "CandidateDetails",
                column: "EducationDegreeId",
                principalTable: "EducationDegreeDetails",
                principalColumn: "EducationDegreeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateDetails_EducationDegreeDetails_EducationDegreeId",
                table: "CandidateDetails");

            migrationBuilder.DropIndex(
                name: "IX_CandidateDetails_EducationDegreeId",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "EducationDegreeId",
                table: "CandidateDetails");
        }
    }
}

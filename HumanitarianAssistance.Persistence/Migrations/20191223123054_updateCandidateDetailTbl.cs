using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateCandidateDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "CandidateDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_EmployeeID",
                table: "CandidateDetails",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateDetails_EmployeeDetail_EmployeeID",
                table: "CandidateDetails",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateDetails_EmployeeDetail_EmployeeID",
                table: "CandidateDetails");

            migrationBuilder.DropIndex(
                name: "IX_CandidateDetails_EmployeeID",
                table: "CandidateDetails");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "CandidateDetails");
        }
    }
}

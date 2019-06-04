using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addRelationForEmployeeInterviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RatingBasedCriteria_InterviewDetailsId",
                table: "RatingBasedCriteria",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewDetails_EmployeeID",
                table: "InterviewDetails",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewDetails_EmployeeDetail_EmployeeID",
                table: "InterviewDetails",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingBasedCriteria_InterviewDetails_InterviewDetailsId",
                table: "RatingBasedCriteria",
                column: "InterviewDetailsId",
                principalTable: "InterviewDetails",
                principalColumn: "InterviewDetailsId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterviewDetails_EmployeeDetail_EmployeeID",
                table: "InterviewDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingBasedCriteria_InterviewDetails_InterviewDetailsId",
                table: "RatingBasedCriteria");

            migrationBuilder.DropIndex(
                name: "IX_RatingBasedCriteria_InterviewDetailsId",
                table: "RatingBasedCriteria");

            migrationBuilder.DropIndex(
                name: "IX_InterviewDetails_EmployeeID",
                table: "InterviewDetails");
        }
    }
}

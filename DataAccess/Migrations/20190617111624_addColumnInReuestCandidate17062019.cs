using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addColumnInReuestCandidate17062019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "HiringRequestCandidates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidates_EmployeeID",
                table: "HiringRequestCandidates",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_HiringRequestCandidates_EmployeeDetail_EmployeeID",
                table: "HiringRequestCandidates",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HiringRequestCandidates_EmployeeDetail_EmployeeID",
                table: "HiringRequestCandidates");

            migrationBuilder.DropIndex(
                name: "IX_HiringRequestCandidates_EmployeeID",
                table: "HiringRequestCandidates");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "HiringRequestCandidates");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addForeignKeyJobGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_Grade",
                table: "EmployeeContract",
                column: "Grade");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContract_JobGrade_Grade",
                table: "EmployeeContract",
                column: "Grade",
                principalTable: "JobGrade",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContract_JobGrade_Grade",
                table: "EmployeeContract");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeContract_Grade",
                table: "EmployeeContract");
        }
    }
}

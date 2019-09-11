using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addForeignKeyOnEmployeeDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluation_EmployeeId",
                table: "EmployeeEvaluation",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEvaluation_EmployeeDetail_EmployeeId",
                table: "EmployeeEvaluation",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEvaluation_EmployeeDetail_EmployeeId",
                table: "EmployeeEvaluation");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeEvaluation_EmployeeId",
                table: "EmployeeEvaluation");
        }
    }
}

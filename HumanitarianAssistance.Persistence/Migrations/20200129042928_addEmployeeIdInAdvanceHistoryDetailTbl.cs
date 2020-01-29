using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addEmployeeIdInAdvanceHistoryDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "AdvanceHistoryDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceHistoryDetail_EmployeeId",
                table: "AdvanceHistoryDetail",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvanceHistoryDetail_EmployeeDetail_EmployeeId",
                table: "AdvanceHistoryDetail",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvanceHistoryDetail_EmployeeDetail_EmployeeId",
                table: "AdvanceHistoryDetail");

            migrationBuilder.DropIndex(
                name: "IX_AdvanceHistoryDetail_EmployeeId",
                table: "AdvanceHistoryDetail");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AdvanceHistoryDetail");
        }
    }
}

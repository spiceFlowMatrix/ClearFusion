using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DataTransfer12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHistoryDetail_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeHistoryDetail_EmployeeID",
                table: "EmployeeHistoryDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryDetail_EmployeeID",
                table: "EmployeeHistoryDetail",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHistoryDetail_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryDetail",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

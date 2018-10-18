using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AdvancesEmployeeIdForeignKeyAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Advances_EmployeeId",
                table: "Advances",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advances_EmployeeDetail_EmployeeId",
                table: "Advances",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advances_EmployeeDetail_EmployeeId",
                table: "Advances");

            migrationBuilder.DropIndex(
                name: "IX_Advances_EmployeeId",
                table: "Advances");
        }
    }
}

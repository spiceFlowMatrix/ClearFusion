using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class employeeEducationForeignKeyRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEducations_EmployeeDetail_EmployeeID",
                table: "EmployeeEducations");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeEducations_EmployeeID",
                table: "EmployeeEducations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducations_EmployeeID",
                table: "EmployeeEducations",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEducations_EmployeeDetail_EmployeeID",
                table: "EmployeeEducations",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

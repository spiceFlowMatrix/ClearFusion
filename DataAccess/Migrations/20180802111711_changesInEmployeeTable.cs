using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class changesInEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetail_EmployeePensionRate_EmployeePensionRateId",
                table: "EmployeeDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetail_EmployeePensionRateId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "EmployeePensionRateId",
                table: "EmployeeDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeePensionRateId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_EmployeePensionRateId",
                table: "EmployeeDetail",
                column: "EmployeePensionRateId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetail_EmployeePensionRate_EmployeePensionRateId",
                table: "EmployeeDetail",
                column: "EmployeePensionRateId",
                principalTable: "EmployeePensionRate",
                principalColumn: "EmployeePensionRateId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

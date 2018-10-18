using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AdvancesColumnAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdvanceId",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RecoveredAmount",
                table: "Advances",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMonthlyAttendance_AdvanceId",
                table: "EmployeeMonthlyAttendance",
                column: "AdvanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMonthlyAttendance_Advances_AdvanceId",
                table: "EmployeeMonthlyAttendance",
                column: "AdvanceId",
                principalTable: "Advances",
                principalColumn: "AdvancesId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMonthlyAttendance_Advances_AdvanceId",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMonthlyAttendance_AdvanceId",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "AdvanceId",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "RecoveredAmount",
                table: "Advances");
        }
    }
}

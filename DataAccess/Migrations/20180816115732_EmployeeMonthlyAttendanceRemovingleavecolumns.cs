using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeMonthlyAttendanceRemovingleavecolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CasualLeaveHours",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "EmergencyLeaveHours",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "MaternityLeaveHours",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.RenameColumn(
                name: "MedicalLeaveHours",
                table: "EmployeeMonthlyAttendance",
                newName: "LeaveHours");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LeaveHours",
                table: "EmployeeMonthlyAttendance",
                newName: "MedicalLeaveHours");

            migrationBuilder.AddColumn<int>(
                name: "CasualLeaveHours",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmergencyLeaveHours",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaternityLeaveHours",
                table: "EmployeeMonthlyAttendance",
                nullable: true);
        }
    }
}

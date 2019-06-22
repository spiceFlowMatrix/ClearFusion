using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addAttendanceMinutesAndOvertimeMinutesinEmployeeAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttendanceMinutes",
                table: "EmployeeMonthlyAttendance",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OverTimeMinutes",
                table: "EmployeeMonthlyAttendance",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OverTimeMinutes",
                table: "EmployeeAttendance",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkTimeMinutes",
                table: "EmployeeAttendance",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendanceMinutes",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "OverTimeMinutes",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "OverTimeMinutes",
                table: "EmployeeAttendance");

            migrationBuilder.DropColumn(
                name: "WorkTimeMinutes",
                table: "EmployeeAttendance");
        }
    }
}

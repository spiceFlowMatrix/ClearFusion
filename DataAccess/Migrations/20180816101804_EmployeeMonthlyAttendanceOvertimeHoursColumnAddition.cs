using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeMonthlyAttendanceOvertimeHoursColumnAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OvertimeHours",
                table: "EmployeeMonthlyAttendance",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OvertimeHours",
                table: "EmployeeMonthlyAttendance");
        }
    }
}

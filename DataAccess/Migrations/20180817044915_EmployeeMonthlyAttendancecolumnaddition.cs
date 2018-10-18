using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeMonthlyAttendancecolumnaddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Casual",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Emergency",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Maternity",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Medical",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "EmployeeMonthlyAttendance",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegCode",
                table: "EmployeeMonthlyAttendance",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Casual",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "Emergency",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "Maternity",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "Medical",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "EmployeeMonthlyAttendance");

            migrationBuilder.DropColumn(
                name: "RegCode",
                table: "EmployeeMonthlyAttendance");
        }
    }
}

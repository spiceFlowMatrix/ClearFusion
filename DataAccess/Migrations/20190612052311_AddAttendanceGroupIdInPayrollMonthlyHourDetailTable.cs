using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddAttendanceGroupIdInPayrollMonthlyHourDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AttendanceGroupId",
                table: "PayrollMonthlyHourDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayrollMonthlyHourDetail_AttendanceGroupId",
                table: "PayrollMonthlyHourDetail",
                column: "AttendanceGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_PayrollMonthlyHourDetail_AttendanceGroupMaster_AttendanceGroupId",
                table: "PayrollMonthlyHourDetail",
                column: "AttendanceGroupId",
                principalTable: "AttendanceGroupMaster",
                principalColumn: "AttendanceGroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayrollMonthlyHourDetail_AttendanceGroupMaster_AttendanceGroupId",
                table: "PayrollMonthlyHourDetail");

            migrationBuilder.DropIndex(
                name: "IX_PayrollMonthlyHourDetail_AttendanceGroupId",
                table: "PayrollMonthlyHourDetail");

            migrationBuilder.DropColumn(
                name: "AttendanceGroupId",
                table: "PayrollMonthlyHourDetail");
        }
    }
}

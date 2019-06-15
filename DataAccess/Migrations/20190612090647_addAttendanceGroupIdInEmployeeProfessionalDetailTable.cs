using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addAttendanceGroupIdInEmployeeProfessionalDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AttendanceGroupId",
                table: "EmployeeProfessionalDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_AttendanceGroupId",
                table: "EmployeeProfessionalDetail",
                column: "AttendanceGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProfessionalDetail_AttendanceGroupMaster_AttendanceGroupId",
                table: "EmployeeProfessionalDetail",
                column: "AttendanceGroupId",
                principalTable: "AttendanceGroupMaster",
                principalColumn: "AttendanceGroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProfessionalDetail_AttendanceGroupMaster_AttendanceGroupId",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeProfessionalDetail_AttendanceGroupId",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropColumn(
                name: "AttendanceGroupId",
                table: "EmployeeProfessionalDetail");
        }
    }
}

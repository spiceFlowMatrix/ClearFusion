using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class dropEmployeeMonthlyAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeMonthlyAttendance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeMonthlyAttendance",
                columns: table => new
                {
                    MonthlyAttendanceId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AbsentHours = table.Column<int>(nullable: true),
                    AttendanceHours = table.Column<int>(nullable: true),
                    CasualLeaveHours = table.Column<int>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    DeputationHours = table.Column<int>(nullable: true),
                    EmergencyLeaveHours = table.Column<int>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    LeaveMonth = table.Column<int>(nullable: true),
                    LeaveYear = table.Column<int>(nullable: true),
                    MaternityLeaveHours = table.Column<int>(nullable: true),
                    MedicalLeaveHours = table.Column<int>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Sent = table.Column<bool>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    TotalDuration = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMonthlyAttendance", x => x.MonthlyAttendanceId);
                    table.ForeignKey(
                        name: "FK_EmployeeMonthlyAttendance_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeMonthlyAttendance_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeMonthlyAttendance_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMonthlyAttendance_CreatedById",
                table: "EmployeeMonthlyAttendance",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMonthlyAttendance_EmployeeId",
                table: "EmployeeMonthlyAttendance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMonthlyAttendance_ModifiedById",
                table: "EmployeeMonthlyAttendance",
                column: "ModifiedById");
        }
    }
}

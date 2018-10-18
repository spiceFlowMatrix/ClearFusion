using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class CreateEmployeeMonthlyAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeMonthlyAttendance",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    MonthlyAttendanceId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeId = table.Column<int>(nullable: true),
                    Month = table.Column<int>(nullable: true),
                    Year = table.Column<int>(nullable: true),
                    AttendanceHours = table.Column<int>(nullable: true),
                    CasualLeaveHours = table.Column<int>(nullable: true),
                    MedicalLeaveHours = table.Column<int>(nullable: true),
                    EmergencyLeaveHours = table.Column<int>(nullable: true),
                    MaternityLeaveHours = table.Column<int>(nullable: true),
                    AbsentHours = table.Column<int>(nullable: true),
                    DeputationHours = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    Sent = table.Column<bool>(nullable: true),
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeMonthlyAttendance");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeMonthlyAttendanceIsApprovedColumnAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "EmployeePaymentTypes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "EmployeeMonthlyAttendance",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "EmployeeMonthlyAttendance");
        }
    }
}

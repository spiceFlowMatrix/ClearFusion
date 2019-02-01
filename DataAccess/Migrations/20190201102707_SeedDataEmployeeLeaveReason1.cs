using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class SeedDataEmployeeLeaveReason1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveReasonDetail",
                keyColumn: "LeaveReasonId",
                keyValue: 1,
                column: "CreatedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LeaveReasonDetail",
                keyColumn: "LeaveReasonId",
                keyValue: 2,
                column: "CreatedDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "LeaveReasonDetail",
                keyColumn: "LeaveReasonId",
                keyValue: 3,
                column: "CreatedDate",
                value: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveReasonDetail",
                keyColumn: "LeaveReasonId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2019, 2, 1, 14, 26, 31, 321, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "LeaveReasonDetail",
                keyColumn: "LeaveReasonId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2019, 2, 1, 14, 26, 31, 322, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "LeaveReasonDetail",
                keyColumn: "LeaveReasonId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2019, 2, 1, 14, 26, 31, 322, DateTimeKind.Local));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class SeedDataEmployeeLeaveReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 22,
                column: "IsDeleted",
                value: true);

            migrationBuilder.UpdateData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 28,
                column: "IsDeleted",
                value: true);

            migrationBuilder.InsertData(
                table: "ApplicationPages",
                columns: new[] { "PageId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ModuleId", "ModuleName", "PageName" },
                values: new object[] { 72, null, null, false, null, null, 6, "Marketing", "Producer" });

            migrationBuilder.UpdateData(
                table: "FinancialYearDetail",
                keyColumn: "FinancialYearId",
                keyValue: 1,
                column: "FinancialYearName",
                value: "2019 Financial Year");

            migrationBuilder.InsertData(
                table: "LeaveReasonDetail",
                columns: new[] { "LeaveReasonId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ReasonName", "Unit" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2019, 2, 1, 14, 26, 31, 321, DateTimeKind.Local), false, null, null, "Casual Leave", 12 },
                    { 2, null, new DateTime(2019, 2, 1, 14, 26, 31, 322, DateTimeKind.Local), false, null, null, "Emergency Leave", 6 },
                    { 3, null, new DateTime(2019, 2, 1, 14, 26, 31, 322, DateTimeKind.Local), false, null, null, "Maternity Leave", 90 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "LeaveReasonDetail",
                keyColumn: "LeaveReasonId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LeaveReasonDetail",
                keyColumn: "LeaveReasonId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LeaveReasonDetail",
                keyColumn: "LeaveReasonId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 22,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 28,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "FinancialYearDetail",
                keyColumn: "FinancialYearId",
                keyValue: 1,
                column: "FinancialYearName",
                value: null);
        }
    }
}

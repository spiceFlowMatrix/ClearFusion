using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeHistoryOutsideOrganizationChangeMonthlySalaryFromDoubleToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHistoryOutsideOrganization_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryOutsideOrganization");

            migrationBuilder.AlterColumn<string>(
                name: "MonthlySalary",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmploymentTo",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmploymentFrom",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHistoryOutsideOrganization_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryOutsideOrganization",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHistoryOutsideOrganization_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryOutsideOrganization");

            migrationBuilder.AlterColumn<double>(
                name: "MonthlySalary",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmploymentTo",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmploymentFrom",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHistoryOutsideOrganization_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryOutsideOrganization",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeHistoryOutsideCountryChangeDatatypeOfMonthlySalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHistoryOutsideCountry_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryOutsideCountry");

            migrationBuilder.AlterColumn<string>(
                name: "MonthlySalary",
                table: "EmployeeHistoryOutsideCountry",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmploymentTo",
                table: "EmployeeHistoryOutsideCountry",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmploymentFrom",
                table: "EmployeeHistoryOutsideCountry",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "EmployeeHistoryOutsideCountry",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHistoryOutsideCountry_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryOutsideCountry",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHistoryOutsideCountry_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryOutsideCountry");

            migrationBuilder.AlterColumn<double>(
                name: "MonthlySalary",
                table: "EmployeeHistoryOutsideCountry",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmploymentTo",
                table: "EmployeeHistoryOutsideCountry",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmploymentFrom",
                table: "EmployeeHistoryOutsideCountry",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "EmployeeHistoryOutsideCountry",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHistoryOutsideCountry_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryOutsideCountry",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

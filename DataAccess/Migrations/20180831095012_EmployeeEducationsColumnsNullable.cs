using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeEducationsColumnsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEducations_EmployeeDetail_EmployeeID",
                table: "EmployeeEducations");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "EmployeeEducations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EducationTo",
                table: "EmployeeEducations",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EducationFrom",
                table: "EmployeeEducations",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEducations_EmployeeDetail_EmployeeID",
                table: "EmployeeEducations",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEducations_EmployeeDetail_EmployeeID",
                table: "EmployeeEducations");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "EmployeeEducations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EducationTo",
                table: "EmployeeEducations",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EducationFrom",
                table: "EmployeeEducations",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEducations_EmployeeDetail_EmployeeID",
                table: "EmployeeEducations",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateAdvancesColumnsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advances_EmployeeDetail_ApprovedBy",
                table: "Advances");

            migrationBuilder.DropForeignKey(
                name: "FK_Advances_CurrencyDetails_CurrencyId",
                table: "Advances");

            migrationBuilder.AlterColumn<int>(
                name: "OfficeId",
                table: "Advances",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "Advances",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeductedDate",
                table: "Advances",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "Advances",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ApprovedBy",
                table: "Advances",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AdvanceRecoveryDate",
                table: "Advances",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddForeignKey(
                name: "FK_Advances_EmployeeDetail_ApprovedBy",
                table: "Advances",
                column: "ApprovedBy",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Advances_CurrencyDetails_CurrencyId",
                table: "Advances",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advances_EmployeeDetail_ApprovedBy",
                table: "Advances");

            migrationBuilder.DropForeignKey(
                name: "FK_Advances_CurrencyDetails_CurrencyId",
                table: "Advances");

            migrationBuilder.AlterColumn<int>(
                name: "OfficeId",
                table: "Advances",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "Advances",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeductedDate",
                table: "Advances",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "Advances",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApprovedBy",
                table: "Advances",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AdvanceRecoveryDate",
                table: "Advances",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Advances_EmployeeDetail_ApprovedBy",
                table: "Advances",
                column: "ApprovedBy",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Advances_CurrencyDetails_CurrencyId",
                table: "Advances",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

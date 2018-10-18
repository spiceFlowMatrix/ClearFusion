using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DataTransfer7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rate",
                table: "ExchangeRates",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ExchangeRates",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "ExchangeRates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficeCode",
                table: "ExchangeRates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "ExchangeRates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountNote",
                table: "AccountType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BalanceType",
                table: "AccountType",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "OfficeCode",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "AccountNote",
                table: "AccountType");

            migrationBuilder.DropColumn(
                name: "BalanceType",
                table: "AccountType");

            migrationBuilder.AlterColumn<double>(
                name: "Rate",
                table: "ExchangeRates",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ExchangeRates",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}

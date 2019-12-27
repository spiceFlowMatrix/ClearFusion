using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class GainLossSelectedAccountsFieldsAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ComparisionDate",
                table: "GainLossSelectedAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreditAccountId",
                table: "GainLossSelectedAccounts",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DebitAccountId",
                table: "GainLossSelectedAccounts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "GainLossSelectedAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "GainLossSelectedAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_CreditAccountId",
                table: "GainLossSelectedAccounts",
                column: "CreditAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_DebitAccountId",
                table: "GainLossSelectedAccounts",
                column: "DebitAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_GainLossSelectedAccounts_ChartOfAccountNew_CreditAccountId",
                table: "GainLossSelectedAccounts",
                column: "CreditAccountId",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GainLossSelectedAccounts_ChartOfAccountNew_DebitAccountId",
                table: "GainLossSelectedAccounts",
                column: "DebitAccountId",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GainLossSelectedAccounts_ChartOfAccountNew_CreditAccountId",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_GainLossSelectedAccounts_ChartOfAccountNew_DebitAccountId",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropIndex(
                name: "IX_GainLossSelectedAccounts_CreditAccountId",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropIndex(
                name: "IX_GainLossSelectedAccounts_DebitAccountId",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropColumn(
                name: "ComparisionDate",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropColumn(
                name: "CreditAccountId",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropColumn(
                name: "DebitAccountId",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "GainLossSelectedAccounts");
        }
    }
}

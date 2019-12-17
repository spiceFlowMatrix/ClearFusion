using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class GainLossSelectedAccountsTblUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GainLossSelectedAccounts_ChartOfAccountNew_ChartOfAccountNe~",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropIndex(
                name: "IX_GainLossSelectedAccounts_ChartOfAccountNewId",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountNewId",
                table: "GainLossSelectedAccounts");

            migrationBuilder.AddColumn<long>(
                name: "LogisticRequestId",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "GainLossSelectedAccounts",
                nullable: true);

            migrationBuilder.AddColumn<long[]>(
                name: "SelectedAccounts",
                table: "GainLossSelectedAccounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GainLossSelectedAccounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_LogisticRequestId",
                table: "StoreItemPurchases",
                column: "LogisticRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_EmployeeId",
                table: "GainLossSelectedAccounts",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GainLossSelectedAccounts_EmployeeDetail_EmployeeId",
                table: "GainLossSelectedAccounts",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItemPurchases_ProjectLogisticRequests_LogisticRequestId",
                table: "StoreItemPurchases",
                column: "LogisticRequestId",
                principalTable: "ProjectLogisticRequests",
                principalColumn: "LogisticRequestsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GainLossSelectedAccounts_EmployeeDetail_EmployeeId",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreItemPurchases_ProjectLogisticRequests_LogisticRequestId",
                table: "StoreItemPurchases");

            migrationBuilder.DropIndex(
                name: "IX_StoreItemPurchases_LogisticRequestId",
                table: "StoreItemPurchases");

            migrationBuilder.DropIndex(
                name: "IX_GainLossSelectedAccounts_EmployeeId",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropColumn(
                name: "LogisticRequestId",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropColumn(
                name: "SelectedAccounts",
                table: "GainLossSelectedAccounts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GainLossSelectedAccounts");

            migrationBuilder.AddColumn<long>(
                name: "ChartOfAccountNewId",
                table: "GainLossSelectedAccounts",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_ChartOfAccountNewId",
                table: "GainLossSelectedAccounts",
                column: "ChartOfAccountNewId");

            migrationBuilder.AddForeignKey(
                name: "FK_GainLossSelectedAccounts_ChartOfAccountNew_ChartOfAccountNe~",
                table: "GainLossSelectedAccounts",
                column: "ChartOfAccountNewId",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

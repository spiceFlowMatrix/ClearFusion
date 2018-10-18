using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class VoucherTransDetailsDataType1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactionDetails_ChartAccountDetail_CreditAccount",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactionDetails_ChartAccountDetail_DebitAccount",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactionDetails_FinancialYearDetail_FinancialYearId",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactionDetails_CreditAccount",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactionDetails_DebitAccount",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactionDetails_FinancialYearId",
                table: "VoucherTransactionDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                table: "VoucherTransactionDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "VoucherTransactionDetails",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<int>(
                name: "ChartAccountDetailAccountCode",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_AccountNo",
                table: "VoucherTransactionDetails",
                column: "AccountNo");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_ChartAccountDetailAccountCode",
                table: "VoucherTransactionDetails",
                column: "ChartAccountDetailAccountCode");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactionDetails_ChartAccountDetail_AccountNo",
                table: "VoucherTransactionDetails",
                column: "AccountNo",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactionDetails_ChartAccountDetail_ChartAccountDetailAccountCode",
                table: "VoucherTransactionDetails",
                column: "ChartAccountDetailAccountCode",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactionDetails_ChartAccountDetail_AccountNo",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactionDetails_ChartAccountDetail_ChartAccountDetailAccountCode",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactionDetails_AccountNo",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactionDetails_ChartAccountDetailAccountCode",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "ChartAccountDetailAccountCode",
                table: "VoucherTransactionDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                table: "VoucherTransactionDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "VoucherTransactionDetails",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_CreditAccount",
                table: "VoucherTransactionDetails",
                column: "CreditAccount");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_DebitAccount",
                table: "VoucherTransactionDetails",
                column: "DebitAccount");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_FinancialYearId",
                table: "VoucherTransactionDetails",
                column: "FinancialYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactionDetails_ChartAccountDetail_CreditAccount",
                table: "VoucherTransactionDetails",
                column: "CreditAccount",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactionDetails_ChartAccountDetail_DebitAccount",
                table: "VoucherTransactionDetails",
                column: "DebitAccount",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactionDetails_FinancialYearDetail_FinancialYearId",
                table: "VoucherTransactionDetails",
                column: "FinancialYearId",
                principalTable: "FinancialYearDetail",
                principalColumn: "FinancialYearId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

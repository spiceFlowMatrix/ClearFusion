using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class exchangerateIX : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "VoucherTransactionDetails");

            //migrationBuilder.AddColumn<int>(
            //    name: "ChartAccountDetailAccountCode",
            //    table: "VoucherTransactions",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_VoucherTransactions_ChartAccountDetailAccountCode",
            //    table: "VoucherTransactions",
            //    column: "ChartAccountDetailAccountCode");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_Date",
                table: "ExchangeRates",
                column: "Date");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_VoucherTransactions_ChartAccountDetail_ChartAccountDetailAccountCode",
            //    table: "VoucherTransactions",
            //    column: "ChartAccountDetailAccountCode",
            //    principalTable: "ChartAccountDetail",
            //    principalColumn: "AccountCode",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactions_ChartAccountDetail_ChartAccountDetailAccountCode",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_ChartAccountDetailAccountCode",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_Date",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "ChartAccountDetailAccountCode",
                table: "VoucherTransactions");

            migrationBuilder.CreateTable(
                name: "VoucherTransactionDetails",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AFGAmount = table.Column<double>(nullable: true),
                    AccountNo = table.Column<int>(nullable: true),
                    Amount = table.Column<double>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    CostBook = table.Column<string>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Credit = table.Column<float>(nullable: true),
                    CreditAccount = table.Column<int>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    Debit = table.Column<float>(nullable: true),
                    DebitAccount = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Donor = table.Column<string>(nullable: true),
                    EURAmount = table.Column<double>(nullable: true),
                    FinancialYearId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Job = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    PKRAmount = table.Column<double>(nullable: true),
                    Program = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: true),
                    USDAmount = table.Column<double>(nullable: true),
                    VoucherNo = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherTransactionDetails", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_VoucherTransactionDetails_ChartAccountDetail_AccountNo",
                        column: x => x.AccountNo,
                        principalTable: "ChartAccountDetail",
                        principalColumn: "AccountCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactionDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactionDetails_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactionDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactionDetails_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactionDetails_VoucherDetail_VoucherNo",
                        column: x => x.VoucherNo,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_AccountNo",
                table: "VoucherTransactionDetails",
                column: "AccountNo");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_CreatedById",
                table: "VoucherTransactionDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_CurrencyId",
                table: "VoucherTransactionDetails",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_ModifiedById",
                table: "VoucherTransactionDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_OfficeId",
                table: "VoucherTransactionDetails",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_VoucherNo",
                table: "VoucherTransactionDetails",
                column: "VoucherNo");
        }
    }
}

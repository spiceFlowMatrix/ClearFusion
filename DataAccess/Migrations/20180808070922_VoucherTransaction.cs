using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class VoucherTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactionDetails_ChartAccountDetail_ChartAccountDetailAccountCode",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactionDetails_ChartAccountDetailAccountCode",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "ChartAccountDetailAccountCode",
                table: "VoucherTransactionDetails");

            migrationBuilder.CreateTable(
                name: "VoucherTransactions",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    TransactionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    VoucherNo = table.Column<long>(nullable: true),
                    CreditAccount = table.Column<int>(nullable: true),
                    DebitAccount = table.Column<int>(nullable: true),
                    Amount = table.Column<double>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    FinancialYearId = table.Column<int>(nullable: true),
                    AccountNo = table.Column<int>(nullable: true),
                    Donor = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    Program = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Job = table.Column<string>(nullable: true),
                    CostBook = table.Column<string>(nullable: true),
                    Debit = table.Column<float>(nullable: true),
                    Credit = table.Column<float>(nullable: true),
                    AFGAmount = table.Column<double>(nullable: true),
                    EURAmount = table.Column<double>(nullable: true),
                    USDAmount = table.Column<double>(nullable: true),
                    PKRAmount = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherTransactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_ChartAccountDetail_AccountNo",
                        column: x => x.AccountNo,
                        principalTable: "ChartAccountDetail",
                        principalColumn: "AccountCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_VoucherDetail_VoucherNo",
                        column: x => x.VoucherNo,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_AccountNo",
                table: "VoucherTransactions",
                column: "AccountNo");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_CreatedById",
                table: "VoucherTransactions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_CurrencyId",
                table: "VoucherTransactions",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_ModifiedById",
                table: "VoucherTransactions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_OfficeId",
                table: "VoucherTransactions",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_VoucherNo",
                table: "VoucherTransactions",
                column: "VoucherNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoucherTransactions");

            migrationBuilder.AddColumn<int>(
                name: "ChartAccountDetailAccountCode",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_ChartAccountDetailAccountCode",
                table: "VoucherTransactionDetails",
                column: "ChartAccountDetailAccountCode");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactionDetails_ChartAccountDetail_ChartAccountDetailAccountCode",
                table: "VoucherTransactionDetails",
                column: "ChartAccountDetailAccountCode",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class GainLossCalculatorConfigurationTblAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GainLossSelectedAccounts");

            migrationBuilder.CreateTable(
                name: "GainLossCalculatorConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ComparisionDate = table.Column<DateTime>(nullable: false),
                    DebitAccountId = table.Column<long>(nullable: true),
                    CreditAccountId = table.Column<long>(nullable: true),
                    SelectedAccounts = table.Column<long[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GainLossCalculatorConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GainLossCalculatorConfiguration_ChartOfAccountNew_CreditAcc~",
                        column: x => x.CreditAccountId,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GainLossCalculatorConfiguration_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GainLossCalculatorConfiguration_ChartOfAccountNew_DebitAcco~",
                        column: x => x.DebitAccountId,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GainLossCalculatorConfiguration_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GainLossCalculatorConfiguration_CreditAccountId",
                table: "GainLossCalculatorConfiguration",
                column: "CreditAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GainLossCalculatorConfiguration_CurrencyId",
                table: "GainLossCalculatorConfiguration",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_GainLossCalculatorConfiguration_DebitAccountId",
                table: "GainLossCalculatorConfiguration",
                column: "DebitAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GainLossCalculatorConfiguration_EmployeeId",
                table: "GainLossCalculatorConfiguration",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GainLossCalculatorConfiguration");

            migrationBuilder.CreateTable(
                name: "GainLossSelectedAccounts",
                columns: table => new
                {
                    GainLossSelectedAccountId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ComparisionDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreditAccountId = table.Column<long>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    DebitAccountId = table.Column<long>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    SelectedAccounts = table.Column<long[]>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GainLossSelectedAccounts", x => x.GainLossSelectedAccountId);
                    table.ForeignKey(
                        name: "FK_GainLossSelectedAccounts_ChartOfAccountNew_CreditAccountId",
                        column: x => x.CreditAccountId,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GainLossSelectedAccounts_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GainLossSelectedAccounts_ChartOfAccountNew_DebitAccountId",
                        column: x => x.DebitAccountId,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GainLossSelectedAccounts_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_CreditAccountId",
                table: "GainLossSelectedAccounts",
                column: "CreditAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_CurrencyId",
                table: "GainLossSelectedAccounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_DebitAccountId",
                table: "GainLossSelectedAccounts",
                column: "DebitAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_EmployeeId",
                table: "GainLossSelectedAccounts",
                column: "EmployeeId");
        }
    }
}

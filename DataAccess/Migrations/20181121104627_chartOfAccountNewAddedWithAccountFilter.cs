using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class chartOfAccountNewAddedWithAccountFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChartOfAccountNewId",
                table: "VoucherTransactions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChartOfAccountNewId1",
                table: "VoucherTransactions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChartOfAccountNewId",
                table: "VoucherDetail",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountFilterType",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AccountFilterTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountFilterTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountFilterType", x => x.AccountFilterTypeId);
                    table.ForeignKey(
                        name: "FK_AccountFilterType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountFilterType_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChartOfAccountNew",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ChartOfAccountNewId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountName = table.Column<string>(maxLength: 100, nullable: true),
                    ParentID = table.Column<long>(nullable: false),
                    AccountLevelId = table.Column<int>(nullable: false),
                    AccountTypeId = table.Column<int>(nullable: true),
                    AccountFilterTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartOfAccountNew", x => x.ChartOfAccountNewId);
                    table.ForeignKey(
                        name: "FK_ChartOfAccountNew_AccountFilterType_AccountFilterTypeId",
                        column: x => x.AccountFilterTypeId,
                        principalTable: "AccountFilterType",
                        principalColumn: "AccountFilterTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChartOfAccountNew_AccountLevel_AccountLevelId",
                        column: x => x.AccountLevelId,
                        principalTable: "AccountLevel",
                        principalColumn: "AccountLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChartOfAccountNew_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountType",
                        principalColumn: "AccountTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChartOfAccountNew_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChartOfAccountNew_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "PayrollAccountHead",
                keyColumn: "PayrollHeadId",
                keyValue: 2,
                columns: new[] { "PayrollHeadTypeId", "TransactionTypeId" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "PayrollAccountHead",
                keyColumn: "PayrollHeadId",
                keyValue: 3,
                columns: new[] { "PayrollHeadTypeId", "TransactionTypeId" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "PayrollAccountHead",
                keyColumn: "PayrollHeadId",
                keyValue: 4,
                column: "IsDeleted",
                value: true);

            migrationBuilder.UpdateData(
                table: "PayrollAccountHead",
                keyColumn: "PayrollHeadId",
                keyValue: 5,
                columns: new[] { "PayrollHeadTypeId", "TransactionTypeId" },
                values: new object[] { 2, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_ChartOfAccountNewId",
                table: "VoucherTransactions",
                column: "ChartOfAccountNewId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_ChartOfAccountNewId1",
                table: "VoucherTransactions",
                column: "ChartOfAccountNewId1");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_ChartOfAccountNewId",
                table: "VoucherDetail",
                column: "ChartOfAccountNewId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountFilterType_CreatedById",
                table: "AccountFilterType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountFilterType_ModifiedById",
                table: "AccountFilterType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccountNew_AccountFilterTypeId",
                table: "ChartOfAccountNew",
                column: "AccountFilterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccountNew_AccountLevelId",
                table: "ChartOfAccountNew",
                column: "AccountLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccountNew_AccountTypeId",
                table: "ChartOfAccountNew",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccountNew_CreatedById",
                table: "ChartOfAccountNew",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccountNew_ModifiedById",
                table: "ChartOfAccountNew",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetail_ChartOfAccountNew_ChartOfAccountNewId",
                table: "VoucherDetail",
                column: "ChartOfAccountNewId",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactions_ChartOfAccountNew_ChartOfAccountNewId",
                table: "VoucherTransactions",
                column: "ChartOfAccountNewId",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactions_ChartOfAccountNew_ChartOfAccountNewId1",
                table: "VoucherTransactions",
                column: "ChartOfAccountNewId1",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetail_ChartOfAccountNew_ChartOfAccountNewId",
                table: "VoucherDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactions_ChartOfAccountNew_ChartOfAccountNewId",
                table: "VoucherTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactions_ChartOfAccountNew_ChartOfAccountNewId1",
                table: "VoucherTransactions");

            migrationBuilder.DropTable(
                name: "ChartOfAccountNew");

            migrationBuilder.DropTable(
                name: "AccountFilterType");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_ChartOfAccountNewId",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_ChartOfAccountNewId1",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherDetail_ChartOfAccountNewId",
                table: "VoucherDetail");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountNewId",
                table: "VoucherTransactions");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountNewId1",
                table: "VoucherTransactions");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountNewId",
                table: "VoucherDetail");

            migrationBuilder.UpdateData(
                table: "PayrollAccountHead",
                keyColumn: "PayrollHeadId",
                keyValue: 2,
                columns: new[] { "PayrollHeadTypeId", "TransactionTypeId" },
                values: new object[] { 3, 2 });

            migrationBuilder.UpdateData(
                table: "PayrollAccountHead",
                keyColumn: "PayrollHeadId",
                keyValue: 3,
                columns: new[] { "PayrollHeadTypeId", "TransactionTypeId" },
                values: new object[] { 3, 2 });

            migrationBuilder.UpdateData(
                table: "PayrollAccountHead",
                keyColumn: "PayrollHeadId",
                keyValue: 4,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "PayrollAccountHead",
                keyColumn: "PayrollHeadId",
                keyValue: 5,
                columns: new[] { "PayrollHeadTypeId", "TransactionTypeId" },
                values: new object[] { 3, 2 });
        }
    }
}

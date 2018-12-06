using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class afterNewCOAMergeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotesMaster_ChartAccountDetail_AccountCode",
                table: "NotesMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryCreditAccount",
                table: "StoreInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryDebitAccount",
                table: "StoreInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetail_ChartAccountDetail_ChartAccountDetailAccountCode",
                table: "VoucherDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactions_ChartAccountDetail_AccountNo",
                table: "VoucherTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactions_ChartAccountDetail_ChartAccountDetailAccountCode",
                table: "VoucherTransactions");

            migrationBuilder.DropTable(
                name: "ChartAccountDetail");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_AccountNo",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_ChartAccountDetailAccountCode",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_TransactionDate_AccountNo",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherDetail_ChartAccountDetailAccountCode",
                table: "VoucherDetail");

            migrationBuilder.DropIndex(
                name: "IX_NotesMaster_AccountCode",
                table: "NotesMaster");

            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "VoucherTransactions");

            migrationBuilder.DropColumn(
                name: "ChartAccountDetailAccountCode",
                table: "VoucherTransactions");

            migrationBuilder.DropColumn(
                name: "ChartAccountDetailAccountCode",
                table: "VoucherDetail");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "AccountCode",
                table: "NotesMaster");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountCode",
                table: "CategoryPopulator");

            migrationBuilder.AlterColumn<long>(
                name: "DebitAccount",
                table: "VoucherTransactions",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreditAccount",
                table: "VoucherTransactions",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChartOfAccountNewId",
                table: "VoucherTransactions",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "InventoryDebitAccount",
                table: "StoreInventories",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "InventoryCreditAccount",
                table: "StoreInventories",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChartOfAccountNewId",
                table: "PaymentTypes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ChartOfAccountNewId",
                table: "NotesMaster",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ChartOfAccountCodeNew",
                table: "CategoryPopulator",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_ChartOfAccountNewId",
                table: "VoucherTransactions",
                column: "ChartOfAccountNewId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_TransactionDate_ChartOfAccountNewId",
                table: "VoucherTransactions",
                columns: new[] { "TransactionDate", "ChartOfAccountNewId" });

            migrationBuilder.CreateIndex(
                name: "IX_NotesMaster_ChartOfAccountNewId",
                table: "NotesMaster",
                column: "ChartOfAccountNewId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotesMaster_ChartOfAccountNew_ChartOfAccountNewId",
                table: "NotesMaster",
                column: "ChartOfAccountNewId",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventories_ChartOfAccountNew_InventoryCreditAccount",
                table: "StoreInventories",
                column: "InventoryCreditAccount",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventories_ChartOfAccountNew_InventoryDebitAccount",
                table: "StoreInventories",
                column: "InventoryDebitAccount",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactions_ChartOfAccountNew_ChartOfAccountNewId",
                table: "VoucherTransactions",
                column: "ChartOfAccountNewId",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotesMaster_ChartOfAccountNew_ChartOfAccountNewId",
                table: "NotesMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_ChartOfAccountNew_InventoryCreditAccount",
                table: "StoreInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_ChartOfAccountNew_InventoryDebitAccount",
                table: "StoreInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactions_ChartOfAccountNew_ChartOfAccountNewId",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_ChartOfAccountNewId",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_TransactionDate_ChartOfAccountNewId",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_NotesMaster_ChartOfAccountNewId",
                table: "NotesMaster");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountNewId",
                table: "VoucherTransactions");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountNewId",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountNewId",
                table: "NotesMaster");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountCodeNew",
                table: "CategoryPopulator");

            migrationBuilder.AlterColumn<int>(
                name: "DebitAccount",
                table: "VoucherTransactions",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreditAccount",
                table: "VoucherTransactions",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountNo",
                table: "VoucherTransactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChartAccountDetailAccountCode",
                table: "VoucherTransactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChartAccountDetailAccountCode",
                table: "VoucherDetail",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InventoryDebitAccount",
                table: "StoreInventories",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "InventoryCreditAccount",
                table: "StoreInventories",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "PaymentTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountCode",
                table: "NotesMaster",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChartOfAccountCode",
                table: "CategoryPopulator",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ChartAccountDetail",
                columns: table => new
                {
                    AccountCode = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountLevelId = table.Column<int>(nullable: false),
                    AccountName = table.Column<string>(maxLength: 100, nullable: true),
                    AccountNote = table.Column<int>(nullable: true),
                    AccountTypeId = table.Column<int>(nullable: true),
                    ChartOfAccountCode = table.Column<long>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    DepMethod = table.Column<string>(nullable: true),
                    DepRate = table.Column<float>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    MDCode = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ParentID = table.Column<long>(nullable: false),
                    Show = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartAccountDetail", x => x.AccountCode);
                    table.ForeignKey(
                        name: "FK_ChartAccountDetail_AccountLevel_AccountLevelId",
                        column: x => x.AccountLevelId,
                        principalTable: "AccountLevel",
                        principalColumn: "AccountLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChartAccountDetail_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountType",
                        principalColumn: "AccountTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChartAccountDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChartAccountDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_AccountNo",
                table: "VoucherTransactions",
                column: "AccountNo");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_ChartAccountDetailAccountCode",
                table: "VoucherTransactions",
                column: "ChartAccountDetailAccountCode");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_TransactionDate_AccountNo",
                table: "VoucherTransactions",
                columns: new[] { "TransactionDate", "AccountNo" });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_ChartAccountDetailAccountCode",
                table: "VoucherDetail",
                column: "ChartAccountDetailAccountCode");

            migrationBuilder.CreateIndex(
                name: "IX_NotesMaster_AccountCode",
                table: "NotesMaster",
                column: "AccountCode");

            migrationBuilder.CreateIndex(
                name: "IX_ChartAccountDetail_AccountCode",
                table: "ChartAccountDetail",
                column: "AccountCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChartAccountDetail_AccountLevelId",
                table: "ChartAccountDetail",
                column: "AccountLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartAccountDetail_AccountTypeId",
                table: "ChartAccountDetail",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartAccountDetail_CreatedById",
                table: "ChartAccountDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChartAccountDetail_ModifiedById",
                table: "ChartAccountDetail",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_NotesMaster_ChartAccountDetail_AccountCode",
                table: "NotesMaster",
                column: "AccountCode",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryCreditAccount",
                table: "StoreInventories",
                column: "InventoryCreditAccount",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryDebitAccount",
                table: "StoreInventories",
                column: "InventoryDebitAccount",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetail_ChartAccountDetail_ChartAccountDetailAccountCode",
                table: "VoucherDetail",
                column: "ChartAccountDetailAccountCode",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactions_ChartAccountDetail_AccountNo",
                table: "VoucherTransactions",
                column: "AccountNo",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactions_ChartAccountDetail_ChartAccountDetailAccountCode",
                table: "VoucherTransactions",
                column: "ChartAccountDetailAccountCode",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

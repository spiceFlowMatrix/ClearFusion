using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class purchaseTableModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssetTypeId",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BudgetLineId",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNo",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiptTypeId",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceivedFromLocation",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VoucherDate",
                table: "StoreItemPurchases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "VoucherId",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReceiptType",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ReceiptTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ReceiptTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptType", x => x.ReceiptTypeId);
                    table.ForeignKey(
                        name: "FK_ReceiptType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptType_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatusAtTimeOfIssue",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StatusAtTimeOfIssueId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusAtTimeOfIssue", x => x.StatusAtTimeOfIssueId);
                    table.ForeignKey(
                        name: "FK_StatusAtTimeOfIssue_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatusAtTimeOfIssue_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_BudgetLineId",
                table: "StoreItemPurchases",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_ProjectId",
                table: "StoreItemPurchases",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_ReceiptTypeId",
                table: "StoreItemPurchases",
                column: "ReceiptTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_Status",
                table: "StoreItemPurchases",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_VoucherId",
                table: "StoreItemPurchases",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptType_CreatedById",
                table: "ReceiptType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptType_ModifiedById",
                table: "ReceiptType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StatusAtTimeOfIssue_CreatedById",
                table: "StatusAtTimeOfIssue",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StatusAtTimeOfIssue_ModifiedById",
                table: "StatusAtTimeOfIssue",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItemPurchases_ProjectBudgetLine_BudgetLineId",
                table: "StoreItemPurchases",
                column: "BudgetLineId",
                principalTable: "ProjectBudgetLine",
                principalColumn: "BudgetLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItemPurchases_ProjectDetails_ProjectId",
                table: "StoreItemPurchases",
                column: "ProjectId",
                principalTable: "ProjectDetails",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItemPurchases_ReceiptType_ReceiptTypeId",
                table: "StoreItemPurchases",
                column: "ReceiptTypeId",
                principalTable: "ReceiptType",
                principalColumn: "ReceiptTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItemPurchases_StatusAtTimeOfIssue_Status",
                table: "StoreItemPurchases",
                column: "Status",
                principalTable: "StatusAtTimeOfIssue",
                principalColumn: "StatusAtTimeOfIssueId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItemPurchases_VoucherDetail_VoucherId",
                table: "StoreItemPurchases",
                column: "VoucherId",
                principalTable: "VoucherDetail",
                principalColumn: "VoucherNo",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreItemPurchases_ProjectBudgetLine_BudgetLineId",
                table: "StoreItemPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreItemPurchases_ProjectDetails_ProjectId",
                table: "StoreItemPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreItemPurchases_ReceiptType_ReceiptTypeId",
                table: "StoreItemPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreItemPurchases_StatusAtTimeOfIssue_Status",
                table: "StoreItemPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreItemPurchases_VoucherDetail_VoucherId",
                table: "StoreItemPurchases");

            migrationBuilder.DropTable(
                name: "ReceiptType");

            migrationBuilder.DropTable(
                name: "StatusAtTimeOfIssue");

            migrationBuilder.DropIndex(
                name: "IX_StoreItemPurchases_BudgetLineId",
                table: "StoreItemPurchases");

            migrationBuilder.DropIndex(
                name: "IX_StoreItemPurchases_ProjectId",
                table: "StoreItemPurchases");

            migrationBuilder.DropIndex(
                name: "IX_StoreItemPurchases_ReceiptTypeId",
                table: "StoreItemPurchases");

            migrationBuilder.DropIndex(
                name: "IX_StoreItemPurchases_Status",
                table: "StoreItemPurchases");

            migrationBuilder.DropIndex(
                name: "IX_StoreItemPurchases_VoucherId",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "AssetTypeId",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "BudgetLineId",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "InvoiceNo",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "ReceiptTypeId",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "ReceivedFromLocation",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "VoucherDate",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                table: "StoreItemPurchases");
        }
    }
}

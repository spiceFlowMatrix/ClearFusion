using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class ReturnProcurementDetailTblAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueVoucherNo",
                table: "StorePurchaseOrders");

            migrationBuilder.AddColumn<long>(
                name: "IssueVoucher",
                table: "StorePurchaseOrders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReturnProcurementDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ReturnedDate = table.Column<DateTime>(nullable: false),
                    ReturnedQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnProcurementDetail", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_IssueVoucher",
                table: "StorePurchaseOrders",
                column: "IssueVoucher");

            migrationBuilder.AddForeignKey(
                name: "FK_StorePurchaseOrders_VoucherDetail_IssueVoucher",
                table: "StorePurchaseOrders",
                column: "IssueVoucher",
                principalTable: "VoucherDetail",
                principalColumn: "VoucherNo",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorePurchaseOrders_VoucherDetail_IssueVoucher",
                table: "StorePurchaseOrders");

            migrationBuilder.DropTable(
                name: "ReturnProcurementDetail");

            migrationBuilder.DropIndex(
                name: "IX_StorePurchaseOrders_IssueVoucher",
                table: "StorePurchaseOrders");

            migrationBuilder.DropColumn(
                name: "IssueVoucher",
                table: "StorePurchaseOrders");

            migrationBuilder.AddColumn<string>(
                name: "IssueVoucherNo",
                table: "StorePurchaseOrders",
                nullable: true);
        }
    }
}

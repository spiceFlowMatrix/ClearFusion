using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class changesInReturnProcurementDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PurchaseId",
                table: "ReturnProcurementDetail",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnProcurementDetail_PurchaseId",
                table: "ReturnProcurementDetail",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnProcurementDetail_StoreItemPurchases_PurchaseId",
                table: "ReturnProcurementDetail",
                column: "PurchaseId",
                principalTable: "StoreItemPurchases",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnProcurementDetail_StoreItemPurchases_PurchaseId",
                table: "ReturnProcurementDetail");

            migrationBuilder.DropIndex(
                name: "IX_ReturnProcurementDetail_PurchaseId",
                table: "ReturnProcurementDetail");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "ReturnProcurementDetail");
        }
    }
}

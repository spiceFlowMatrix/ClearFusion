using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class changesInStorePurchaseOrderTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProcurementId",
                table: "ReturnProcurementDetail",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnProcurementDetail_ProcurementId",
                table: "ReturnProcurementDetail",
                column: "ProcurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnProcurementDetail_StorePurchaseOrders_ProcurementId",
                table: "ReturnProcurementDetail",
                column: "ProcurementId",
                principalTable: "StorePurchaseOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnProcurementDetail_StorePurchaseOrders_ProcurementId",
                table: "ReturnProcurementDetail");

            migrationBuilder.DropIndex(
                name: "IX_ReturnProcurementDetail_ProcurementId",
                table: "ReturnProcurementDetail");

            migrationBuilder.DropColumn(
                name: "ProcurementId",
                table: "ReturnProcurementDetail");
        }
    }
}

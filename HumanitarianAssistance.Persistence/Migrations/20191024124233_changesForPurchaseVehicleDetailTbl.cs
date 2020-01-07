using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class changesForPurchaseVehicleDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleItemDetail_PurchaseId",
                table: "VehicleItemDetail");

            migrationBuilder.DropIndex(
                name: "IX_VehicleItemDetail_VehiclePurchaseId",
                table: "VehicleItemDetail");

            migrationBuilder.DropIndex(
                name: "IX_GeneratorItemDetail_GeneratorPurchaseId",
                table: "GeneratorItemDetail");

            migrationBuilder.DropIndex(
                name: "IX_GeneratorItemDetail_PurchaseId",
                table: "GeneratorItemDetail");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleItemDetail_PurchaseId",
                table: "VehicleItemDetail",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleItemDetail_VehiclePurchaseId",
                table: "VehicleItemDetail",
                column: "VehiclePurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorItemDetail_GeneratorPurchaseId",
                table: "GeneratorItemDetail",
                column: "GeneratorPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorItemDetail_PurchaseId",
                table: "GeneratorItemDetail",
                column: "PurchaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleItemDetail_PurchaseId",
                table: "VehicleItemDetail");

            migrationBuilder.DropIndex(
                name: "IX_VehicleItemDetail_VehiclePurchaseId",
                table: "VehicleItemDetail");

            migrationBuilder.DropIndex(
                name: "IX_GeneratorItemDetail_GeneratorPurchaseId",
                table: "GeneratorItemDetail");

            migrationBuilder.DropIndex(
                name: "IX_GeneratorItemDetail_PurchaseId",
                table: "GeneratorItemDetail");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleItemDetail_PurchaseId",
                table: "VehicleItemDetail",
                column: "PurchaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleItemDetail_VehiclePurchaseId",
                table: "VehicleItemDetail",
                column: "VehiclePurchaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorItemDetail_GeneratorPurchaseId",
                table: "GeneratorItemDetail",
                column: "GeneratorPurchaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorItemDetail_PurchaseId",
                table: "GeneratorItemDetail",
                column: "PurchaseId",
                unique: true);
        }
    }
}

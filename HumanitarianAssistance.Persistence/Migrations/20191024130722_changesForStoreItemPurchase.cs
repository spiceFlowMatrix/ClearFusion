using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class changesForStoreItemPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleItemDetail_PurchaseId",
                table: "VehicleItemDetail");

            migrationBuilder.DropIndex(
                name: "IX_GeneratorItemDetail_PurchaseId",
                table: "GeneratorItemDetail");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleItemDetail_PurchaseId",
                table: "VehicleItemDetail",
                column: "PurchaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorItemDetail_PurchaseId",
                table: "GeneratorItemDetail",
                column: "PurchaseId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleItemDetail_PurchaseId",
                table: "VehicleItemDetail");

            migrationBuilder.DropIndex(
                name: "IX_GeneratorItemDetail_PurchaseId",
                table: "GeneratorItemDetail");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleItemDetail_PurchaseId",
                table: "VehicleItemDetail",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratorItemDetail_PurchaseId",
                table: "GeneratorItemDetail",
                column: "PurchaseId");
        }
    }
}

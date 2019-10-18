using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class purchasedVehicleDetailForeignKeyChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleMileageDetail_VehicleId",
                table: "VehicleMileageDetail");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMileageDetail_VehicleId",
                table: "VehicleMileageDetail",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleMileageDetail_VehicleId",
                table: "VehicleMileageDetail");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMileageDetail_VehicleId",
                table: "VehicleMileageDetail",
                column: "VehicleId",
                unique: true);
        }
    }
}

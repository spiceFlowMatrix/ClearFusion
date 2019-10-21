using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class changesInPurchasedVehicleDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "StartingMileage",
                table: "PurchasedVehicleDetail",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "MobilOilConsumptionRate",
                table: "PurchasedVehicleDetail",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "IncurredMileage",
                table: "PurchasedVehicleDetail",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "FuelConsumptionRate",
                table: "PurchasedVehicleDetail",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "Voltage",
                table: "PurchasedGeneratorDetail",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "StartingUsage",
                table: "PurchasedGeneratorDetail",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "MobilOilConsumptionRate",
                table: "PurchasedGeneratorDetail",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "IncurredUsage",
                table: "PurchasedGeneratorDetail",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "FuelConsumptionRate",
                table: "PurchasedGeneratorDetail",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StartingMileage",
                table: "PurchasedVehicleDetail",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "MobilOilConsumptionRate",
                table: "PurchasedVehicleDetail",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "IncurredMileage",
                table: "PurchasedVehicleDetail",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "FuelConsumptionRate",
                table: "PurchasedVehicleDetail",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "Voltage",
                table: "PurchasedGeneratorDetail",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "StartingUsage",
                table: "PurchasedGeneratorDetail",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "MobilOilConsumptionRate",
                table: "PurchasedGeneratorDetail",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "IncurredUsage",
                table: "PurchasedGeneratorDetail",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "FuelConsumptionRate",
                table: "PurchasedGeneratorDetail",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}

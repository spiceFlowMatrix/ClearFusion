using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class UpdatedLogisticAndStoreTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "PurchaseUnitType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ChasisNo",
                table: "PurchasedVehicleDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EngineNo",
                table: "PurchasedVehicleDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManufacturerCountry",
                table: "PurchasedVehicleDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonRemarks",
                table: "PurchasedVehicleDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationNo",
                table: "PurchasedVehicleDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChasisNo",
                table: "PurchasedGeneratorDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "PurchasedGeneratorDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EngineNo",
                table: "PurchasedGeneratorDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManufacturerCountry",
                table: "PurchasedGeneratorDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonRemarks",
                table: "PurchasedGeneratorDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationNo",
                table: "PurchasedGeneratorDetail",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "ProjectLogisticItems",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedGeneratorDetail_EmployeeID",
                table: "PurchasedGeneratorDetail",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedGeneratorDetail_EmployeeDetail_EmployeeID",
                table: "PurchasedGeneratorDetail",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedGeneratorDetail_EmployeeDetail_EmployeeID",
                table: "PurchasedGeneratorDetail");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedGeneratorDetail_EmployeeID",
                table: "PurchasedGeneratorDetail");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "PurchaseUnitType");

            migrationBuilder.DropColumn(
                name: "ChasisNo",
                table: "PurchasedVehicleDetail");

            migrationBuilder.DropColumn(
                name: "EngineNo",
                table: "PurchasedVehicleDetail");

            migrationBuilder.DropColumn(
                name: "ManufacturerCountry",
                table: "PurchasedVehicleDetail");

            migrationBuilder.DropColumn(
                name: "PersonRemarks",
                table: "PurchasedVehicleDetail");

            migrationBuilder.DropColumn(
                name: "RegistrationNo",
                table: "PurchasedVehicleDetail");

            migrationBuilder.DropColumn(
                name: "ChasisNo",
                table: "PurchasedGeneratorDetail");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "PurchasedGeneratorDetail");

            migrationBuilder.DropColumn(
                name: "EngineNo",
                table: "PurchasedGeneratorDetail");

            migrationBuilder.DropColumn(
                name: "ManufacturerCountry",
                table: "PurchasedGeneratorDetail");

            migrationBuilder.DropColumn(
                name: "PersonRemarks",
                table: "PurchasedGeneratorDetail");

            migrationBuilder.DropColumn(
                name: "RegistrationNo",
                table: "PurchasedGeneratorDetail");

            migrationBuilder.AlterColumn<long>(
                name: "Quantity",
                table: "ProjectLogisticItems",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}

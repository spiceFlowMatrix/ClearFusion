using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addNewTablesInStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchasedGeneratorDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Voltage = table.Column<int>(nullable: false),
                    StartingUsage = table.Column<int>(nullable: false),
                    IncurredUsage = table.Column<int>(nullable: false),
                    FuelConsumptionRate = table.Column<int>(nullable: false),
                    MobilOilConsumptionRate = table.Column<int>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false),
                    ModelYear = table.Column<int>(nullable: false),
                    PurchaseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasedGeneratorDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasedGeneratorDetail_StoreItemPurchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "StoreItemPurchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchasedVehicleDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PlateNo = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    StartingMileage = table.Column<int>(nullable: false),
                    IncurredMileage = table.Column<int>(nullable: false),
                    FuelConsumptionRate = table.Column<int>(nullable: false),
                    MobilOilConsumptionRate = table.Column<int>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false),
                    ModelYear = table.Column<int>(nullable: false),
                    PurchaseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasedVehicleDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasedVehicleDetail_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasedVehicleDetail_StoreItemPurchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "StoreItemPurchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneratorItemDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PurchaseId = table.Column<long>(nullable: false),
                    GeneratorPurchaseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratorItemDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratorItemDetail_PurchasedGeneratorDetail_GeneratorPurch~",
                        column: x => x.GeneratorPurchaseId,
                        principalTable: "PurchasedGeneratorDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneratorItemDetail_StoreItemPurchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "StoreItemPurchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleItemDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PurchaseId = table.Column<long>(nullable: false),
                    VehiclePurchaseId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleItemDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleItemDetail_StoreItemPurchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "StoreItemPurchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleItemDetail_PurchasedVehicleDetail_VehiclePurchaseId",
                        column: x => x.VehiclePurchaseId,
                        principalTable: "PurchasedVehicleDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedGeneratorDetail_PurchaseId",
                table: "PurchasedGeneratorDetail",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedVehicleDetail_EmployeeId",
                table: "PurchasedVehicleDetail",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedVehicleDetail_PurchaseId",
                table: "PurchasedVehicleDetail",
                column: "PurchaseId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratorItemDetail");

            migrationBuilder.DropTable(
                name: "VehicleItemDetail");

            migrationBuilder.DropTable(
                name: "PurchasedGeneratorDetail");

            migrationBuilder.DropTable(
                name: "PurchasedVehicleDetail");
        }
    }
}

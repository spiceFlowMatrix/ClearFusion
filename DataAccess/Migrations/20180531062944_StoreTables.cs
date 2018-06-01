using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class StoreTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventoryItemType",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemType = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItemType", x => x.ItemType);
                    table.ForeignKey(
                        name: "FK_InventoryItemType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItemType_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseUnitType",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UnitTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UnitTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseUnitType", x => x.UnitTypeId);
                    table.ForeignKey(
                        name: "FK_PurchaseUnitType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseUnitType_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreInventories",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    InventoryId = table.Column<string>(nullable: false),
                    InventoryCode = table.Column<string>(nullable: true),
                    InventoryDescription = table.Column<string>(nullable: true),
                    InventoryAccount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreInventories", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_StoreInventories_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreInventories_ChartAccountDetail_InventoryAccount",
                        column: x => x.InventoryAccount,
                        principalTable: "ChartAccountDetail",
                        principalColumn: "AccountCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreInventories_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemId = table.Column<string>(nullable: false),
                    ItemInventory = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(nullable: true),
                    ItemCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Voucher = table.Column<long>(nullable: false),
                    ItemType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_InventoryItems_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_StoreInventories_ItemInventory",
                        column: x => x.ItemInventory,
                        principalTable: "StoreInventories",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_InventoryItemType_ItemType",
                        column: x => x.ItemType,
                        principalTable: "InventoryItemType",
                        principalColumn: "ItemType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryItems_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_VoucherDetail_Voucher",
                        column: x => x.Voucher,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreItemPurchases",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PurchaseId = table.Column<string>(nullable: false),
                    SerialNo = table.Column<string>(nullable: false),
                    InventoryItem = table.Column<string>(nullable: false),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    UnitType = table.Column<int>(nullable: false),
                    UnitCost = table.Column<long>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ApplyDepreciation = table.Column<bool>(nullable: false),
                    DepreciationRate = table.Column<long>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    ImageGuid = table.Column<string>(nullable: true),
                    ImageName = table.Column<string>(nullable: true),
                    ImageFileType = table.Column<string>(nullable: true),
                    ImageFileName = table.Column<string>(nullable: true),
                    PurchasedById = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItemPurchases", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_CurrencyDetails_Currency",
                        column: x => x.Currency,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_InventoryItems_InventoryItem",
                        column: x => x.InventoryItem,
                        principalTable: "InventoryItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_EmployeeDetail_PurchasedById",
                        column: x => x.PurchasedById,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_PurchaseUnitType_UnitType",
                        column: x => x.UnitType,
                        principalTable: "PurchaseUnitType",
                        principalColumn: "UnitTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPurchaseDocuments",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DocumentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DocumentName = table.Column<string>(nullable: true),
                    File = table.Column<byte[]>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    DocumentGuid = table.Column<string>(nullable: true),
                    Purchase = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPurchaseDocuments", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_ItemPurchaseDocuments_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemPurchaseDocuments_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemPurchaseDocuments_StoreItemPurchases_Purchase",
                        column: x => x.Purchase,
                        principalTable: "StoreItemPurchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseGenerators",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    GeneratorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Purchase = table.Column<string>(nullable: false),
                    GeneratorBrand = table.Column<string>(nullable: true),
                    GeneratorModel = table.Column<string>(nullable: true),
                    MakeYear = table.Column<string>(nullable: true),
                    SerialBarCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseGenerators", x => x.GeneratorId);
                    table.ForeignKey(
                        name: "FK_PurchaseGenerators_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseGenerators_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseGenerators_StoreItemPurchases_Purchase",
                        column: x => x.Purchase,
                        principalTable: "StoreItemPurchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseVehicles",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    VehicleId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Purchase = table.Column<string>(nullable: true),
                    VehicleDescription = table.Column<string>(nullable: true),
                    VehicleBrand = table.Column<string>(nullable: true),
                    VehicleModel = table.Column<string>(nullable: true),
                    VehicleMakeYear = table.Column<string>(nullable: true),
                    VehiclePlate = table.Column<string>(nullable: true),
                    VehicleSerialNo = table.Column<string>(nullable: true),
                    VehicleImageName = table.Column<string>(nullable: true),
                    VehicleImageFileName = table.Column<string>(nullable: true),
                    VehicleImageFileType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseVehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_PurchaseVehicles_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseVehicles_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseVehicles_StoreItemPurchases_Purchase",
                        column: x => x.Purchase,
                        principalTable: "StoreItemPurchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StorePurchaseOrders",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrderId = table.Column<string>(nullable: false),
                    Purchase = table.Column<string>(nullable: true),
                    InventoryItem = table.Column<string>(nullable: true),
                    IssuedQuantity = table.Column<int>(nullable: false),
                    MustReturn = table.Column<bool>(nullable: false),
                    IssuedToEmployeeId = table.Column<int>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    ReturnedDate = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorePurchaseOrders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_StorePurchaseOrders_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StorePurchaseOrders_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StorePurchaseOrders_InventoryItems_InventoryItem",
                        column: x => x.InventoryItem,
                        principalTable: "InventoryItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StorePurchaseOrders_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StorePurchaseOrders_StoreItemPurchases_Purchase",
                        column: x => x.Purchase,
                        principalTable: "StoreItemPurchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Vehicle = table.Column<int>(nullable: false),
                    Latitude = table.Column<long>(nullable: false),
                    Longitude = table.Column<long>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleLocations_PurchaseVehicles_Vehicle",
                        column: x => x.Vehicle,
                        principalTable: "PurchaseVehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleMileages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Vehicle = table.Column<int>(nullable: false),
                    Mileage = table.Column<long>(nullable: false),
                    Verified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleMileages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleMileages_PurchaseVehicles_Vehicle",
                        column: x => x.Vehicle,
                        principalTable: "PurchaseVehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotorMaintenances",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MaintenanceId = table.Column<string>(nullable: false),
                    Order = table.Column<string>(nullable: true),
                    Generator = table.Column<int>(nullable: false),
                    Vehicle = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    StoreName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorMaintenances", x => x.MaintenanceId);
                    table.ForeignKey(
                        name: "FK_MotorMaintenances_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorMaintenances_PurchaseGenerators_Generator",
                        column: x => x.Generator,
                        principalTable: "PurchaseGenerators",
                        principalColumn: "GeneratorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotorMaintenances_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorMaintenances_StorePurchaseOrders_Order",
                        column: x => x.Order,
                        principalTable: "StorePurchaseOrders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorMaintenances_PurchaseVehicles_Vehicle",
                        column: x => x.Vehicle,
                        principalTable: "PurchaseVehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotorSpareParts",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PartId = table.Column<string>(nullable: false),
                    Order = table.Column<string>(nullable: true),
                    Generator = table.Column<int>(nullable: false),
                    Vehicle = table.Column<int>(nullable: false),
                    PartName = table.Column<string>(nullable: true),
                    PartDescription = table.Column<string>(nullable: true),
                    PartUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorSpareParts", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_MotorSpareParts_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorSpareParts_PurchaseGenerators_Generator",
                        column: x => x.Generator,
                        principalTable: "PurchaseGenerators",
                        principalColumn: "GeneratorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotorSpareParts_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorSpareParts_StorePurchaseOrders_Order",
                        column: x => x.Order,
                        principalTable: "StorePurchaseOrders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorSpareParts_PurchaseVehicles_Vehicle",
                        column: x => x.Vehicle,
                        principalTable: "PurchaseVehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleFuel",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FuelId = table.Column<string>(nullable: false),
                    Order = table.Column<string>(nullable: true),
                    Vehicle = table.Column<int>(nullable: false),
                    Generator = table.Column<int>(nullable: false),
                    FuelQuantity = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleFuel", x => x.FuelId);
                    table.ForeignKey(
                        name: "FK_VehicleFuel_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleFuel_PurchaseGenerators_Generator",
                        column: x => x.Generator,
                        principalTable: "PurchaseGenerators",
                        principalColumn: "GeneratorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleFuel_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleFuel_StorePurchaseOrders_Order",
                        column: x => x.Order,
                        principalTable: "StorePurchaseOrders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleFuel_PurchaseVehicles_Vehicle",
                        column: x => x.Vehicle,
                        principalTable: "PurchaseVehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_CreatedById",
                table: "InventoryItems",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ItemInventory",
                table: "InventoryItems",
                column: "ItemInventory");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ItemType",
                table: "InventoryItems",
                column: "ItemType");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ModifiedById",
                table: "InventoryItems",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_Voucher",
                table: "InventoryItems",
                column: "Voucher");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItemType_CreatedById",
                table: "InventoryItemType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItemType_ModifiedById",
                table: "InventoryItemType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPurchaseDocuments_CreatedById",
                table: "ItemPurchaseDocuments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPurchaseDocuments_ModifiedById",
                table: "ItemPurchaseDocuments",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPurchaseDocuments_Purchase",
                table: "ItemPurchaseDocuments",
                column: "Purchase");

            migrationBuilder.CreateIndex(
                name: "IX_MotorMaintenances_CreatedById",
                table: "MotorMaintenances",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MotorMaintenances_Generator",
                table: "MotorMaintenances",
                column: "Generator");

            migrationBuilder.CreateIndex(
                name: "IX_MotorMaintenances_ModifiedById",
                table: "MotorMaintenances",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_MotorMaintenances_Order",
                table: "MotorMaintenances",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_MotorMaintenances_Vehicle",
                table: "MotorMaintenances",
                column: "Vehicle");

            migrationBuilder.CreateIndex(
                name: "IX_MotorSpareParts_CreatedById",
                table: "MotorSpareParts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MotorSpareParts_Generator",
                table: "MotorSpareParts",
                column: "Generator");

            migrationBuilder.CreateIndex(
                name: "IX_MotorSpareParts_ModifiedById",
                table: "MotorSpareParts",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_MotorSpareParts_Order",
                table: "MotorSpareParts",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_MotorSpareParts_Vehicle",
                table: "MotorSpareParts",
                column: "Vehicle");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseGenerators_CreatedById",
                table: "PurchaseGenerators",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseGenerators_ModifiedById",
                table: "PurchaseGenerators",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseGenerators_Purchase",
                table: "PurchaseGenerators",
                column: "Purchase");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseUnitType_CreatedById",
                table: "PurchaseUnitType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseUnitType_ModifiedById",
                table: "PurchaseUnitType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseVehicles_CreatedById",
                table: "PurchaseVehicles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseVehicles_ModifiedById",
                table: "PurchaseVehicles",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseVehicles_Purchase",
                table: "PurchaseVehicles",
                column: "Purchase");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventories_CreatedById",
                table: "StoreInventories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventories_InventoryAccount",
                table: "StoreInventories",
                column: "InventoryAccount");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventories_ModifiedById",
                table: "StoreInventories",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_CreatedById",
                table: "StoreItemPurchases",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_Currency",
                table: "StoreItemPurchases",
                column: "Currency");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_InventoryItem",
                table: "StoreItemPurchases",
                column: "InventoryItem");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_ModifiedById",
                table: "StoreItemPurchases",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_PurchasedById",
                table: "StoreItemPurchases",
                column: "PurchasedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_UnitType",
                table: "StoreItemPurchases",
                column: "UnitType");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_CreatedById",
                table: "StorePurchaseOrders",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_EmployeeId",
                table: "StorePurchaseOrders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_InventoryItem",
                table: "StorePurchaseOrders",
                column: "InventoryItem");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_ModifiedById",
                table: "StorePurchaseOrders",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_Purchase",
                table: "StorePurchaseOrders",
                column: "Purchase");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFuel_CreatedById",
                table: "VehicleFuel",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFuel_Generator",
                table: "VehicleFuel",
                column: "Generator");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFuel_ModifiedById",
                table: "VehicleFuel",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFuel_Order",
                table: "VehicleFuel",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFuel_Vehicle",
                table: "VehicleFuel",
                column: "Vehicle");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleLocations_Vehicle",
                table: "VehicleLocations",
                column: "Vehicle");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMileages_Vehicle",
                table: "VehicleMileages",
                column: "Vehicle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPurchaseDocuments");

            migrationBuilder.DropTable(
                name: "MotorMaintenances");

            migrationBuilder.DropTable(
                name: "MotorSpareParts");

            migrationBuilder.DropTable(
                name: "VehicleFuel");

            migrationBuilder.DropTable(
                name: "VehicleLocations");

            migrationBuilder.DropTable(
                name: "VehicleMileages");

            migrationBuilder.DropTable(
                name: "PurchaseGenerators");

            migrationBuilder.DropTable(
                name: "StorePurchaseOrders");

            migrationBuilder.DropTable(
                name: "PurchaseVehicles");

            migrationBuilder.DropTable(
                name: "StoreItemPurchases");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "PurchaseUnitType");

            migrationBuilder.DropTable(
                name: "StoreInventories");

            migrationBuilder.DropTable(
                name: "InventoryItemType");
        }
    }
}

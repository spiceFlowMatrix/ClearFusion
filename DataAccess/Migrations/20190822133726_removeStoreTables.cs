using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class removeStoreTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "StoreItemGroups");

            migrationBuilder.DropTable(
                name: "StoreInventories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseUnitType",
                columns: table => new
                {
                    UnitTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
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
                    InventoryId = table.Column<string>(nullable: false),
                    AssetType = table.Column<int>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    InventoryCode = table.Column<string>(nullable: true),
                    InventoryCreditAccount = table.Column<long>(nullable: true),
                    InventoryDebitAccount = table.Column<long>(nullable: false),
                    InventoryDescription = table.Column<string>(nullable: true),
                    InventoryName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
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
                        name: "FK_StoreInventories_ChartOfAccountNew_InventoryCreditAccount",
                        column: x => x.InventoryCreditAccount,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreInventories_ChartOfAccountNew_InventoryDebitAccount",
                        column: x => x.InventoryDebitAccount,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreInventories_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreItemGroups",
                columns: table => new
                {
                    ItemGroupId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    InventoryId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ItemGroupCode = table.Column<string>(nullable: true),
                    ItemGroupName = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItemGroups", x => x.ItemGroupId);
                    table.ForeignKey(
                        name: "FK_StoreItemGroups_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemGroups_StoreInventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "StoreInventories",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemGroups_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    ItemId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ItemCode = table.Column<string>(nullable: true),
                    ItemGroupId = table.Column<long>(nullable: true),
                    ItemInventory = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(nullable: true),
                    ItemType = table.Column<int>(nullable: false),
                    MasterInventoryCode = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true)
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
                        name: "FK_InventoryItems_StoreItemGroups_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "StoreItemGroups",
                        principalColumn: "ItemGroupId",
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
                });

            migrationBuilder.CreateTable(
                name: "StoreItemPurchases",
                columns: table => new
                {
                    PurchaseId = table.Column<string>(nullable: false),
                    ApplyDepreciation = table.Column<bool>(nullable: false),
                    AssetTypeId = table.Column<int>(nullable: true),
                    BudgetLineId = table.Column<long>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Currency = table.Column<int>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    DepreciationRate = table.Column<double>(nullable: false),
                    ImageFileName = table.Column<string>(nullable: true),
                    ImageFileType = table.Column<string>(nullable: true),
                    InventoryItem = table.Column<string>(nullable: false),
                    InvoiceDate = table.Column<DateTime>(nullable: true),
                    InvoiceFileName = table.Column<string>(nullable: true),
                    InvoiceFileType = table.Column<string>(nullable: true),
                    InvoiceNo = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    IsPurchaseVerified = table.Column<bool>(nullable: false),
                    JournalCode = table.Column<int>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    PaymentTypeId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    PurchasedById = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ReceiptTypeId = table.Column<int>(nullable: true),
                    ReceivedFromLocation = table.Column<string>(nullable: true),
                    SerialNo = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: true),
                    UnitCost = table.Column<long>(nullable: false),
                    UnitType = table.Column<int>(nullable: false),
                    VerifiedPurchaseVoucher = table.Column<long>(nullable: true),
                    VoucherDate = table.Column<DateTime>(nullable: false),
                    VoucherId = table.Column<long>(nullable: true)
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
                        name: "FK_StoreItemPurchases_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_EmployeeDetail_PurchasedById",
                        column: x => x.PurchasedById,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_ReceiptType_ReceiptTypeId",
                        column: x => x.ReceiptTypeId,
                        principalTable: "ReceiptType",
                        principalColumn: "ReceiptTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_StatusAtTimeOfIssue_Status",
                        column: x => x.Status,
                        principalTable: "StatusAtTimeOfIssue",
                        principalColumn: "StatusAtTimeOfIssueId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_PurchaseUnitType_UnitType",
                        column: x => x.UnitType,
                        principalTable: "PurchaseUnitType",
                        principalColumn: "UnitTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_VoucherDetail_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemPurchaseDocuments",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    DocumentGuid = table.Column<string>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    File = table.Column<byte[]>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
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
                    GeneratorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    GeneratorBrand = table.Column<string>(nullable: true),
                    GeneratorModel = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    MakeYear = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Purchase = table.Column<string>(nullable: false),
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
                    VehicleId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Purchase = table.Column<string>(nullable: true),
                    VehicleBrand = table.Column<string>(nullable: true),
                    VehicleDescription = table.Column<string>(nullable: true),
                    VehicleImageFileName = table.Column<string>(nullable: true),
                    VehicleImageFileType = table.Column<string>(nullable: true),
                    VehicleImageName = table.Column<string>(nullable: true),
                    VehicleMakeYear = table.Column<string>(nullable: true),
                    VehicleModel = table.Column<string>(nullable: true),
                    VehiclePlate = table.Column<string>(nullable: true),
                    VehicleSerialNo = table.Column<string>(nullable: true)
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
                    OrderId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    InventoryItem = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    IssedToLocation = table.Column<string>(nullable: true),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    IssueVoucherNo = table.Column<string>(nullable: true),
                    IssuedQuantity = table.Column<int>(nullable: false),
                    IssuedToEmployeeId = table.Column<int>(nullable: false),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    MustReturn = table.Column<bool>(nullable: false),
                    Project = table.Column<long>(nullable: false),
                    Purchase = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    Returned = table.Column<bool>(nullable: false),
                    ReturnedDate = table.Column<DateTime>(nullable: true),
                    StatusAtTimeOfIssue = table.Column<int>(nullable: false)
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
                        name: "FK_StorePurchaseOrders_InventoryItems_InventoryItem",
                        column: x => x.InventoryItem,
                        principalTable: "InventoryItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StorePurchaseOrders_EmployeeDetail_IssuedToEmployeeId",
                        column: x => x.IssuedToEmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
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
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Latitude = table.Column<long>(nullable: false),
                    Longitude = table.Column<long>(nullable: false),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Vehicle = table.Column<int>(nullable: false)
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
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Mileage = table.Column<long>(nullable: false),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Vehicle = table.Column<int>(nullable: false),
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
                    MaintenanceId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Generator = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Order = table.Column<string>(nullable: true),
                    StoreName = table.Column<string>(nullable: false),
                    Vehicle = table.Column<int>(nullable: false)
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
                    PartId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    Generator = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Order = table.Column<string>(nullable: true),
                    PartDescription = table.Column<string>(nullable: true),
                    PartName = table.Column<string>(nullable: true),
                    PartUsed = table.Column<bool>(nullable: false),
                    Vehicle = table.Column<int>(nullable: false)
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
                    FuelId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    FuelQuantity = table.Column<long>(nullable: false),
                    Generator = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Order = table.Column<string>(nullable: true),
                    Vehicle = table.Column<int>(nullable: false)
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
                name: "IX_InventoryItems_ItemGroupId",
                table: "InventoryItems",
                column: "ItemGroupId");

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
                name: "IX_StoreInventories_InventoryCreditAccount",
                table: "StoreInventories",
                column: "InventoryCreditAccount");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventories_InventoryDebitAccount",
                table: "StoreInventories",
                column: "InventoryDebitAccount");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventories_ModifiedById",
                table: "StoreInventories",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemGroups_CreatedById",
                table: "StoreItemGroups",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemGroups_InventoryId",
                table: "StoreItemGroups",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemGroups_ModifiedById",
                table: "StoreItemGroups",
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
                name: "IX_StoreItemPurchases_ProjectId",
                table: "StoreItemPurchases",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_PurchasedById",
                table: "StoreItemPurchases",
                column: "PurchasedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_ReceiptTypeId",
                table: "StoreItemPurchases",
                column: "ReceiptTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_Status",
                table: "StoreItemPurchases",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_UnitType",
                table: "StoreItemPurchases",
                column: "UnitType");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_VoucherId",
                table: "StoreItemPurchases",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_CreatedById",
                table: "StorePurchaseOrders",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_InventoryItem",
                table: "StorePurchaseOrders",
                column: "InventoryItem");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_IssuedToEmployeeId",
                table: "StorePurchaseOrders",
                column: "IssuedToEmployeeId");

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
    }
}

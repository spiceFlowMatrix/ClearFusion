using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addItemTypeCategoryInInventoryTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemTypeCategory",
                table: "StoreItemGroups",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTransportCategory",
                table: "StoreInventories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ComparativeStatus",
                table: "ProjectLogisticRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemTypeCategory",
                table: "InventoryItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StoreLogger",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EventType = table.Column<string>(nullable: true),
                    LogText = table.Column<string>(nullable: true),
                    PurchaseId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreLogger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreLogger_StoreItemPurchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "StoreItemPurchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreLogger_PurchaseId",
                table: "StoreLogger",
                column: "PurchaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreLogger");

            migrationBuilder.DropColumn(
                name: "ItemTypeCategory",
                table: "StoreItemGroups");

            migrationBuilder.DropColumn(
                name: "IsTransportCategory",
                table: "StoreInventories");

            migrationBuilder.DropColumn(
                name: "ComparativeStatus",
                table: "ProjectLogisticRequests");

            migrationBuilder.DropColumn(
                name: "ItemTypeCategory",
                table: "InventoryItems");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class dropStoreInventories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_StoreInventories_ItemInventory",
                table: "InventoryItems");

            migrationBuilder.DropTable(
                name: "StoreInventories");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItems_ItemInventory",
                table: "InventoryItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreInventories",
                columns: table => new
                {
                    InventoryId = table.Column<string>(nullable: false),
                    AssetType = table.Column<int>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    InventoryAccount = table.Column<int>(nullable: true),
                    InventoryChartOfAccount = table.Column<long>(nullable: false),
                    InventoryCode = table.Column<string>(nullable: true),
                    InventoryDescription = table.Column<string>(nullable: true),
                    InventoryName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                        name: "FK_StoreInventories_ChartAccountDetail_InventoryAccount",
                        column: x => x.InventoryAccount,
                        principalTable: "ChartAccountDetail",
                        principalColumn: "AccountCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreInventories_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ItemInventory",
                table: "InventoryItems",
                column: "ItemInventory");

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

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_StoreInventories_ItemInventory",
                table: "InventoryItems",
                column: "ItemInventory",
                principalTable: "StoreInventories",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

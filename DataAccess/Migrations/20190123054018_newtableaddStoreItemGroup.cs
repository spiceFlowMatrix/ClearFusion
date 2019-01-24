using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class newtableaddStoreItemGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ItemGroupId",
                table: "InventoryItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StoreItemGroups",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ItemGroupId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ItemGroupCode = table.Column<string>(nullable: true),
                    ItemGroupName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    InventoryId = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ItemGroupId",
                table: "InventoryItems",
                column: "ItemGroupId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_StoreItemGroups_ItemGroupId",
                table: "InventoryItems",
                column: "ItemGroupId",
                principalTable: "StoreItemGroups",
                principalColumn: "ItemGroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_StoreItemGroups_ItemGroupId",
                table: "InventoryItems");

            migrationBuilder.DropTable(
                name: "StoreItemGroups");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItems_ItemGroupId",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "ItemGroupId",
                table: "InventoryItems");
        }
    }
}

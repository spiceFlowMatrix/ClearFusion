using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class itemTypeAsNullableInStoreInventoryItemTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_InventoryItemType_ItemType",
                table: "InventoryItems");

            migrationBuilder.AlterColumn<int>(
                name: "ItemType",
                table: "InventoryItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_InventoryItemType_ItemType",
                table: "InventoryItems",
                column: "ItemType",
                principalTable: "InventoryItemType",
                principalColumn: "ItemType",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_InventoryItemType_ItemType",
                table: "InventoryItems");

            migrationBuilder.AlterColumn<int>(
                name: "ItemType",
                table: "InventoryItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_InventoryItemType_ItemType",
                table: "InventoryItems",
                column: "ItemType",
                principalTable: "InventoryItemType",
                principalColumn: "ItemType",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

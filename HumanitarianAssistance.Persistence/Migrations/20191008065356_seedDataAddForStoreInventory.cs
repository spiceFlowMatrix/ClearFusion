using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class seedDataAddForStoreInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_ChartOfAccountNew_InventoryDebitAccount",
                table: "StoreInventories");

            migrationBuilder.AlterColumn<long>(
                name: "InventoryDebitAccount",
                table: "StoreInventories",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventories_ChartOfAccountNew_InventoryDebitAccount",
                table: "StoreInventories",
                column: "InventoryDebitAccount",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_ChartOfAccountNew_InventoryDebitAccount",
                table: "StoreInventories");

            migrationBuilder.AlterColumn<long>(
                name: "InventoryDebitAccount",
                table: "StoreInventories",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventories_ChartOfAccountNew_InventoryDebitAccount",
                table: "StoreInventories",
                column: "InventoryDebitAccount",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

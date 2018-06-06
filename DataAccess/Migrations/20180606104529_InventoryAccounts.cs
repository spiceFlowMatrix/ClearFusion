using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InventoryAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryAccount",
                table: "StoreInventories");

            migrationBuilder.RenameColumn(
                name: "InventoryAccount",
                table: "StoreInventories",
                newName: "InventoryDebitAccount");

            migrationBuilder.RenameIndex(
                name: "IX_StoreInventories_InventoryAccount",
                table: "StoreInventories",
                newName: "IX_StoreInventories_InventoryDebitAccount");

            migrationBuilder.AddColumn<int>(
                name: "InventoryCreditAccount",
                table: "StoreInventories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventories_InventoryCreditAccount",
                table: "StoreInventories",
                column: "InventoryCreditAccount");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryCreditAccount",
                table: "StoreInventories",
                column: "InventoryCreditAccount",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryDebitAccount",
                table: "StoreInventories",
                column: "InventoryDebitAccount",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryCreditAccount",
                table: "StoreInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryDebitAccount",
                table: "StoreInventories");

            migrationBuilder.DropIndex(
                name: "IX_StoreInventories_InventoryCreditAccount",
                table: "StoreInventories");

            migrationBuilder.DropColumn(
                name: "InventoryCreditAccount",
                table: "StoreInventories");

            migrationBuilder.RenameColumn(
                name: "InventoryDebitAccount",
                table: "StoreInventories",
                newName: "InventoryAccount");

            migrationBuilder.RenameIndex(
                name: "IX_StoreInventories_InventoryDebitAccount",
                table: "StoreInventories",
                newName: "IX_StoreInventories_InventoryAccount");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryAccount",
                table: "StoreInventories",
                column: "InventoryAccount",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

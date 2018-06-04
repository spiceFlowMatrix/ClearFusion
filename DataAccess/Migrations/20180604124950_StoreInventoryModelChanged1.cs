using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class StoreInventoryModelChanged1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryAccount",
                table: "StoreInventories");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryAccount",
                table: "StoreInventories",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AssetType",
                table: "StoreInventories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "InventoryChartOfAccount",
                table: "StoreInventories",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "InventoryName",
                table: "StoreInventories",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryAccount",
                table: "StoreInventories",
                column: "InventoryAccount",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryAccount",
                table: "StoreInventories");

            migrationBuilder.DropColumn(
                name: "AssetType",
                table: "StoreInventories");

            migrationBuilder.DropColumn(
                name: "InventoryChartOfAccount",
                table: "StoreInventories");

            migrationBuilder.DropColumn(
                name: "InventoryName",
                table: "StoreInventories");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryAccount",
                table: "StoreInventories",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class StoreInChang2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryAccount",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryAccount",
                table: "StoreInventories");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryAccount",
                table: "StoreInventories",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventories_ChartAccountDetail_InventoryAccount",
                table: "StoreInventories",
                column: "InventoryAccount",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class UpdatedLogisticsSupplierTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierName",
                table: "ProjectLogisticSuppliers");

            migrationBuilder.RenameColumn(
                name: "FinalPrice",
                table: "ProjectLogisticSuppliers",
                newName: "FinalUnitPrice");

            migrationBuilder.AddColumn<long>(
                name: "ItemId",
                table: "ProjectLogisticSuppliers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "StoreSourceCodeId",
                table: "ProjectLogisticSuppliers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticSuppliers_ItemId",
                table: "ProjectLogisticSuppliers",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticSuppliers_StoreSourceCodeId",
                table: "ProjectLogisticSuppliers",
                column: "StoreSourceCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLogisticSuppliers_InventoryItems_ItemId",
                table: "ProjectLogisticSuppliers",
                column: "ItemId",
                principalTable: "InventoryItems",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLogisticSuppliers_StoreSourceCodeDetail_StoreSourceC~",
                table: "ProjectLogisticSuppliers",
                column: "StoreSourceCodeId",
                principalTable: "StoreSourceCodeDetail",
                principalColumn: "SourceCodeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLogisticSuppliers_InventoryItems_ItemId",
                table: "ProjectLogisticSuppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLogisticSuppliers_StoreSourceCodeDetail_StoreSourceC~",
                table: "ProjectLogisticSuppliers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectLogisticSuppliers_ItemId",
                table: "ProjectLogisticSuppliers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectLogisticSuppliers_StoreSourceCodeId",
                table: "ProjectLogisticSuppliers");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ProjectLogisticSuppliers");

            migrationBuilder.DropColumn(
                name: "StoreSourceCodeId",
                table: "ProjectLogisticSuppliers");

            migrationBuilder.RenameColumn(
                name: "FinalUnitPrice",
                table: "ProjectLogisticSuppliers",
                newName: "FinalPrice");

            migrationBuilder.AddColumn<string>(
                name: "SupplierName",
                table: "ProjectLogisticSuppliers",
                nullable: true);
        }
    }
}

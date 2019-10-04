using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addColumnReceivedFromLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ReceivedFromLocation",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_ReceivedFromLocation",
                table: "StoreItemPurchases",
                column: "ReceivedFromLocation");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItemPurchases_StoreSourceCodeDetail_ReceivedFromLocati~",
                table: "StoreItemPurchases",
                column: "ReceivedFromLocation",
                principalTable: "StoreSourceCodeDetail",
                principalColumn: "SourceCodeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreItemPurchases_StoreSourceCodeDetail_ReceivedFromLocati~",
                table: "StoreItemPurchases");

            migrationBuilder.DropIndex(
                name: "IX_StoreItemPurchases_ReceivedFromLocation",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "ReceivedFromLocation",
                table: "StoreItemPurchases");
        }
    }
}

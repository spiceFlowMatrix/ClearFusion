using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class removeColumnReceivedFromLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivedFromLocation",
                table: "StoreItemPurchases");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_BudgetLineId",
                table: "StoreItemPurchases",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_OfficeId",
                table: "StoreItemPurchases",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItemPurchases_ProjectBudgetLineDetail_BudgetLineId",
                table: "StoreItemPurchases",
                column: "BudgetLineId",
                principalTable: "ProjectBudgetLineDetail",
                principalColumn: "BudgetLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItemPurchases_OfficeDetail_OfficeId",
                table: "StoreItemPurchases",
                column: "OfficeId",
                principalTable: "OfficeDetail",
                principalColumn: "OfficeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreItemPurchases_ProjectBudgetLineDetail_BudgetLineId",
                table: "StoreItemPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreItemPurchases_OfficeDetail_OfficeId",
                table: "StoreItemPurchases");

            migrationBuilder.DropIndex(
                name: "IX_StoreItemPurchases_BudgetLineId",
                table: "StoreItemPurchases");

            migrationBuilder.DropIndex(
                name: "IX_StoreItemPurchases_OfficeId",
                table: "StoreItemPurchases");

            migrationBuilder.AddColumn<string>(
                name: "ReceivedFromLocation",
                table: "StoreItemPurchases",
                nullable: true);
        }
    }
}

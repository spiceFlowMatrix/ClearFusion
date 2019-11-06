using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addProjectDetailReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreItemPurchases_ProjectDetails_ProjectId",
                table: "StoreItemPurchases");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItemPurchases_ProjectDetail_ProjectId",
                table: "StoreItemPurchases",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreItemPurchases_ProjectDetail_ProjectId",
                table: "StoreItemPurchases");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItemPurchases_ProjectDetails_ProjectId",
                table: "StoreItemPurchases",
                column: "ProjectId",
                principalTable: "ProjectDetails",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class projectAndBudgetLineRelationAddedVoucherTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_BudgetLineId",
                table: "VoucherTransactions",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_ProjectId",
                table: "VoucherTransactions",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactions_ProjectBudgetLineDetail_BudgetLineId",
                table: "VoucherTransactions",
                column: "BudgetLineId",
                principalTable: "ProjectBudgetLineDetail",
                principalColumn: "BudgetLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactions_ProjectDetail_ProjectId",
                table: "VoucherTransactions",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactions_ProjectBudgetLineDetail_BudgetLineId",
                table: "VoucherTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactions_ProjectDetail_ProjectId",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_BudgetLineId",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_ProjectId",
                table: "VoucherTransactions");
        }
    }
}

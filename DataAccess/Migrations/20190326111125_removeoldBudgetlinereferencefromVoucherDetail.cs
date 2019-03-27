using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class removeoldBudgetlinereferencefromVoucherDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetail_ProjectBudgetLine_BudgetLineId",
                table: "VoucherDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetail_ProjectBudgetLineDetail_BudgetLineId",
                table: "VoucherDetail",
                column: "BudgetLineId",
                principalTable: "ProjectBudgetLineDetail",
                principalColumn: "BudgetLineId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetail_ProjectBudgetLineDetail_BudgetLineId",
                table: "VoucherDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetail_ProjectBudgetLine_BudgetLineId",
                table: "VoucherDetail",
                column: "BudgetLineId",
                principalTable: "ProjectBudgetLine",
                principalColumn: "BudgetLineId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

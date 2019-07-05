using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addForeignKeyInTableProjectDetailandVoucherTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_JobId",
                table: "VoucherTransactions",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactions_ProjectJobDetail_JobId",
                table: "VoucherTransactions",
                column: "JobId",
                principalTable: "ProjectJobDetail",
                principalColumn: "ProjectJobId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactions_ProjectJobDetail_JobId",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_JobId",
                table: "VoucherTransactions");
        }
    }
}

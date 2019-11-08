using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addColumnInProjectLogisticsRequestTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BudgetLineId",
                table: "ProjectLogisticRequests",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "ProjectLogisticRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "ProjectLogisticRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HiringRequestStatus",
                table: "ProjectHiringRequestDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticRequests_BudgetLineId",
                table: "ProjectLogisticRequests",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticRequests_CurrencyId",
                table: "ProjectLogisticRequests",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticRequests_OfficeId",
                table: "ProjectLogisticRequests",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLogisticRequests_ProjectBudgetLineDetail_BudgetLineId",
                table: "ProjectLogisticRequests",
                column: "BudgetLineId",
                principalTable: "ProjectBudgetLineDetail",
                principalColumn: "BudgetLineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLogisticRequests_CurrencyDetails_CurrencyId",
                table: "ProjectLogisticRequests",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectLogisticRequests_OfficeDetail_OfficeId",
                table: "ProjectLogisticRequests",
                column: "OfficeId",
                principalTable: "OfficeDetail",
                principalColumn: "OfficeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLogisticRequests_ProjectBudgetLineDetail_BudgetLineId",
                table: "ProjectLogisticRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLogisticRequests_CurrencyDetails_CurrencyId",
                table: "ProjectLogisticRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectLogisticRequests_OfficeDetail_OfficeId",
                table: "ProjectLogisticRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProjectLogisticRequests_BudgetLineId",
                table: "ProjectLogisticRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProjectLogisticRequests_CurrencyId",
                table: "ProjectLogisticRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProjectLogisticRequests_OfficeId",
                table: "ProjectLogisticRequests");

            migrationBuilder.DropColumn(
                name: "BudgetLineId",
                table: "ProjectLogisticRequests");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "ProjectLogisticRequests");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "ProjectLogisticRequests");

            migrationBuilder.DropColumn(
                name: "HiringRequestStatus",
                table: "ProjectHiringRequestDetail");
        }
    }
}

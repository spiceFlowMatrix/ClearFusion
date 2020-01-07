using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addForeignKeyInFinancialProjectDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialProjectDetail_ProjectDetail_SelectedProjectDetailP~",
                table: "FinancialProjectDetail");

            migrationBuilder.DropIndex(
                name: "IX_FinancialProjectDetail_SelectedProjectDetailProjectId",
                table: "FinancialProjectDetail");

            migrationBuilder.DropColumn(
                name: "SelectedProjectDetailProjectId",
                table: "FinancialProjectDetail");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialProjectDetail_ProjectSelectionId",
                table: "FinancialProjectDetail",
                column: "ProjectSelectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialProjectDetail_ProjectDetail_ProjectSelectionId",
                table: "FinancialProjectDetail",
                column: "ProjectSelectionId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialProjectDetail_ProjectDetail_ProjectSelectionId",
                table: "FinancialProjectDetail");

            migrationBuilder.DropIndex(
                name: "IX_FinancialProjectDetail_ProjectSelectionId",
                table: "FinancialProjectDetail");

            migrationBuilder.AddColumn<long>(
                name: "SelectedProjectDetailProjectId",
                table: "FinancialProjectDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialProjectDetail_SelectedProjectDetailProjectId",
                table: "FinancialProjectDetail",
                column: "SelectedProjectDetailProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialProjectDetail_ProjectDetail_SelectedProjectDetailP~",
                table: "FinancialProjectDetail",
                column: "SelectedProjectDetailProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

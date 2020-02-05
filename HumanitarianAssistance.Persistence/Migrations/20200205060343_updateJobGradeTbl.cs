using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateJobGradeTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChartOfAccountNewId",
                table: "JobGrade",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobGrade_ChartOfAccountNewId",
                table: "JobGrade",
                column: "ChartOfAccountNewId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobGrade_ChartOfAccountNew_ChartOfAccountNewId",
                table: "JobGrade",
                column: "ChartOfAccountNewId",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobGrade_ChartOfAccountNew_ChartOfAccountNewId",
                table: "JobGrade");

            migrationBuilder.DropIndex(
                name: "IX_JobGrade_ChartOfAccountNewId",
                table: "JobGrade");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountNewId",
                table: "JobGrade");
        }
    }
}

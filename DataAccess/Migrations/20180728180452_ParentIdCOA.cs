using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ParentIdCOA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartAccountDetail_ChartAccountDetail_ParentID",
                table: "ChartAccountDetail");

            migrationBuilder.DropIndex(
                name: "IX_ChartAccountDetail_ParentID",
                table: "ChartAccountDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ChartAccountDetail_ParentID",
                table: "ChartAccountDetail",
                column: "ParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartAccountDetail_ChartAccountDetail_ParentID",
                table: "ChartAccountDetail",
                column: "ParentID",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

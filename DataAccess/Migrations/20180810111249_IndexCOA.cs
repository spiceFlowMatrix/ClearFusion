using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class IndexCOA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ChartAccountDetail_AccountCode",
                table: "ChartAccountDetail",
                column: "AccountCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChartAccountDetail_AccountCode",
                table: "ChartAccountDetail");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateProposaltable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "ProjectProposalDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ProjectProposalDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "ProjectProposalDetail");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProjectProposalDetail");
        }
    }
}

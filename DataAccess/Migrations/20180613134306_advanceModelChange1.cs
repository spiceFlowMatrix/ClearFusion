using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class advanceModelChange1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Advances");

            migrationBuilder.DropColumn(
                name: "VoucherReferenceNo",
                table: "Advances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "Advances",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoucherReferenceNo",
                table: "Advances",
                nullable: true);
        }
    }
}

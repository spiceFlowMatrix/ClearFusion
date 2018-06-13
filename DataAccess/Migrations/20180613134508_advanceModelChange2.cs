using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class advanceModelChange2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "Advances",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "VoucherReferenceNo",
                table: "Advances",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Advances");

            migrationBuilder.DropColumn(
                name: "VoucherReferenceNo",
                table: "Advances");
        }
    }
}

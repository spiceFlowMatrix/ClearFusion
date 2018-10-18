using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class VoucherTransDetailsDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketingJob",
                table: "VoucherTransactionDetails");

            migrationBuilder.AddColumn<float>(
                name: "Cr",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Dr",
                table: "VoucherTransactionDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cr",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "Dr",
                table: "VoucherTransactionDetails");

            migrationBuilder.AddColumn<bool>(
                name: "MarketingJob",
                table: "VoucherTransactionDetails",
                nullable: true);
        }
    }
}

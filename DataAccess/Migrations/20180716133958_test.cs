using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountNo",
                table: "VoucherTransactionDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CostBook",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Donor",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "VoucherTransactionDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Job",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MarketingJob",
                table: "VoucherTransactionDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Program",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Project",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RowValidated",
                table: "VoucherTransactionDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Sector",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreItemCode",
                table: "VoucherTransactionDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "CostBook",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "Donor",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "Job",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "MarketingJob",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "Program",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "Project",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "RowValidated",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "Sector",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "StoreItemCode",
                table: "VoucherTransactionDetails");
        }
    }
}

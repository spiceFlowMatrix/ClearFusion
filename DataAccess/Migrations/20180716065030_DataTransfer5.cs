using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DataTransfer5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AddColumn<string>(
				name: "CurrencyCode",
				table: "VoucherDetail",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "VoucherMode",
				table: "VoucherDetail",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "VoucherType",
				table: "VoucherDetail",
				nullable: true);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropColumn(
				name: "CurrencyCode",
				table: "VoucherDetail");

			migrationBuilder.DropColumn(
				name: "VoucherMode",
				table: "VoucherDetail");

			migrationBuilder.DropColumn(
				name: "VoucherType",
				table: "VoucherDetail");
		}
    }
}

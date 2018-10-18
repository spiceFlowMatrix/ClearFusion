using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DataTransfer1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetail_CurrencyDetails_CurrencyId",
                table: "VoucherDetail");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "VoucherDetail",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetail_CurrencyDetails_CurrencyId",
                table: "VoucherDetail",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetail_CurrencyDetails_CurrencyId",
                table: "VoucherDetail");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "VoucherDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetail_CurrencyDetails_CurrencyId",
                table: "VoucherDetail",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

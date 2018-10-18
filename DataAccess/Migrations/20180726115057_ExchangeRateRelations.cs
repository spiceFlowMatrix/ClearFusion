using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ExchangeRateRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_CurrencyDetails_FromCurrency",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_CurrencyDetails_ToCurrency",
                table: "ExchangeRates");

            //migrationBuilder.DropColumn(
            //    name: "InternationalEmploymentList",
            //    table: "EmployeeDetail");

            //migrationBuilder.AddColumn<int>(
            //    name: "WorkingDay",
            //    table: "PayrollMonthlyHourDetail",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "WorkingTime",
            //    table: "PayrollMonthlyHourDetail",
            //    nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ToCurrency",
                table: "ExchangeRates",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FromCurrency",
                table: "ExchangeRates",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_OfficeId",
                table: "ExchangeRates",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_CurrencyDetails_FromCurrency",
                table: "ExchangeRates",
                column: "FromCurrency",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_OfficeDetail_OfficeId",
                table: "ExchangeRates",
                column: "OfficeId",
                principalTable: "OfficeDetail",
                principalColumn: "OfficeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_CurrencyDetails_ToCurrency",
                table: "ExchangeRates",
                column: "ToCurrency",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_CurrencyDetails_FromCurrency",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_OfficeDetail_OfficeId",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_CurrencyDetails_ToCurrency",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_OfficeId",
                table: "ExchangeRates");

            //migrationBuilder.DropColumn(
            //    name: "WorkingDay",
            //    table: "PayrollMonthlyHourDetail");

            //migrationBuilder.DropColumn(
            //    name: "WorkingTime",
            //    table: "PayrollMonthlyHourDetail");

            migrationBuilder.AlterColumn<int>(
                name: "ToCurrency",
                table: "ExchangeRates",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FromCurrency",
                table: "ExchangeRates",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "InternationalEmploymentList",
            //    table: "EmployeeDetail",
            //    nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_CurrencyDetails_FromCurrency",
                table: "ExchangeRates",
                column: "FromCurrency",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_CurrencyDetails_ToCurrency",
                table: "ExchangeRates",
                column: "ToCurrency",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

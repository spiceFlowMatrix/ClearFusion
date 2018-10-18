using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DataTransfer9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ReceivedAmount",
                table: "AnalyticalDetail",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "BLAmount",
                table: "AnalyticalDetail",
                nullable: true,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ReceivedAmount",
                table: "AnalyticalDetail",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "BLAmount",
                table: "AnalyticalDetail",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);
        }
    }
}

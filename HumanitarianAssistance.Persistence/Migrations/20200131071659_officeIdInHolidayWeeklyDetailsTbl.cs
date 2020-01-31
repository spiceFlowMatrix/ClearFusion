using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class officeIdInHolidayWeeklyDetailsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HolidayWeeklyDetails_OfficeDetail_OfficeId",
                table: "HolidayWeeklyDetails");

            migrationBuilder.AlterColumn<int>(
                name: "OfficeId",
                table: "HolidayWeeklyDetails",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_HolidayWeeklyDetails_OfficeDetail_OfficeId",
                table: "HolidayWeeklyDetails",
                column: "OfficeId",
                principalTable: "OfficeDetail",
                principalColumn: "OfficeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HolidayWeeklyDetails_OfficeDetail_OfficeId",
                table: "HolidayWeeklyDetails");

            migrationBuilder.AlterColumn<int>(
                name: "OfficeId",
                table: "HolidayWeeklyDetails",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HolidayWeeklyDetails_OfficeDetail_OfficeId",
                table: "HolidayWeeklyDetails",
                column: "OfficeId",
                principalTable: "OfficeDetail",
                principalColumn: "OfficeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

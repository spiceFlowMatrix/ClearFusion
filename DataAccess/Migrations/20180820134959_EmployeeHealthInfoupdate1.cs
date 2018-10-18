using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeHealthInfoupdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHealthInfo_EmployeeDetail_EmployeeId",
                table: "EmployeeHealthInfo");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeHealthInfo",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHealthInfo_EmployeeDetail_EmployeeId",
                table: "EmployeeHealthInfo",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHealthInfo_EmployeeDetail_EmployeeId",
                table: "EmployeeHealthInfo");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeHealthInfo",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHealthInfo_EmployeeDetail_EmployeeId",
                table: "EmployeeHealthInfo",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

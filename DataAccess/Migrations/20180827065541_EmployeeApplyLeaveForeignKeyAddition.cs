using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeApplyLeaveForeignKeyAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeApplyLeave_EmployeeDetail_EmployeeId",
                table: "EmployeeApplyLeave");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeApplyLeave",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeApplyLeave_EmployeeDetail_EmployeeId",
                table: "EmployeeApplyLeave",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeApplyLeave_EmployeeDetail_EmployeeId",
                table: "EmployeeApplyLeave");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeApplyLeave",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeApplyLeave_EmployeeDetail_EmployeeId",
                table: "EmployeeApplyLeave",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

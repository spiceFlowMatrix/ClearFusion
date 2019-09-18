using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class EmployeeidInUserDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "UserDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OvertimeMinutes",
                table: "EmployeePaymentTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkingMinutes",
                table: "EmployeePaymentTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_EmployeeId",
                table: "UserDetails",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_EmployeeDetail_EmployeeId",
                table: "UserDetails",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_EmployeeDetail_EmployeeId",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_EmployeeId",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "OvertimeMinutes",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropColumn(
                name: "WorkingMinutes",
                table: "EmployeePaymentTypes");
        }
    }
}

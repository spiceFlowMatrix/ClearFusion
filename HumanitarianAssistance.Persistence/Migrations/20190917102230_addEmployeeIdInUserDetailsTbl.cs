using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addEmployeeIdInUserDetailsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "UserDetails",
                nullable: true);

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
        }
    }
}

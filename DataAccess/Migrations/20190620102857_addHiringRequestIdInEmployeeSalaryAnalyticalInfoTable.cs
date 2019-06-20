using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addHiringRequestIdInEmployeeSalaryAnalyticalInfoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "HiringRequestId",
                table: "EmployeeSalaryAnalyticalInfo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_HiringRequestId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "HiringRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaryAnalyticalInfo_ProjectHiringRequestDetail_HiringRequestId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "HiringRequestId",
                principalTable: "ProjectHiringRequestDetail",
                principalColumn: "HiringRequestId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaryAnalyticalInfo_ProjectHiringRequestDetail_HiringRequestId",
                table: "EmployeeSalaryAnalyticalInfo");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_HiringRequestId",
                table: "EmployeeSalaryAnalyticalInfo");

            migrationBuilder.DropColumn(
                name: "HiringRequestId",
                table: "EmployeeSalaryAnalyticalInfo");
        }
    }
}

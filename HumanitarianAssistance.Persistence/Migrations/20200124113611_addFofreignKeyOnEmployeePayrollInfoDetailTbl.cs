using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addFofreignKeyOnEmployeePayrollInfoDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EmployeePayrollInfoDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollInfoDetail_CurrencyId",
                table: "EmployeePayrollInfoDetail",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollInfoDetail_EmployeeId",
                table: "EmployeePayrollInfoDetail",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayrollInfoDetail_CurrencyDetails_CurrencyId",
                table: "EmployeePayrollInfoDetail",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayrollInfoDetail_EmployeeDetail_EmployeeId",
                table: "EmployeePayrollInfoDetail",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayrollInfoDetail_CurrencyDetails_CurrencyId",
                table: "EmployeePayrollInfoDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayrollInfoDetail_EmployeeDetail_EmployeeId",
                table: "EmployeePayrollInfoDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePayrollInfoDetail_CurrencyId",
                table: "EmployeePayrollInfoDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePayrollInfoDetail_EmployeeId",
                table: "EmployeePayrollInfoDetail");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EmployeePayrollInfoDetail");
        }
    }
}

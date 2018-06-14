using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeContractChanges1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BudgetLine",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_BudgetLine",
                table: "EmployeeContract",
                column: "BudgetLine");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_Designation",
                table: "EmployeeContract",
                column: "Designation");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_DutyStation",
                table: "EmployeeContract",
                column: "DutyStation");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_EmployeeId",
                table: "EmployeeContract",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContract_ProjectBudgetLine_BudgetLine",
                table: "EmployeeContract",
                column: "BudgetLine",
                principalTable: "ProjectBudgetLine",
                principalColumn: "BudgetLineId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContract_DesignationDetail_Designation",
                table: "EmployeeContract",
                column: "Designation",
                principalTable: "DesignationDetail",
                principalColumn: "DesignationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContract_OfficeDetail_DutyStation",
                table: "EmployeeContract",
                column: "DutyStation",
                principalTable: "OfficeDetail",
                principalColumn: "OfficeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContract_EmployeeDetail_EmployeeId",
                table: "EmployeeContract",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContract_ProjectBudgetLine_BudgetLine",
                table: "EmployeeContract");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContract_DesignationDetail_Designation",
                table: "EmployeeContract");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContract_OfficeDetail_DutyStation",
                table: "EmployeeContract");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContract_EmployeeDetail_EmployeeId",
                table: "EmployeeContract");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeContract_BudgetLine",
                table: "EmployeeContract");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeContract_Designation",
                table: "EmployeeContract");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeContract_DutyStation",
                table: "EmployeeContract");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeContract_EmployeeId",
                table: "EmployeeContract");

            migrationBuilder.AlterColumn<int>(
                name: "BudgetLine",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}

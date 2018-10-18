using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeContractIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "WorkTime",
                table: "EmployeeContract",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Province",
                table: "EmployeeContract",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Project",
                table: "EmployeeContract",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DutyStation",
                table: "EmployeeContract",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DurationOfContract",
                table: "EmployeeContract",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Designation",
                table: "EmployeeContract",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Country",
                table: "EmployeeContract",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "ContractPeriod",
                table: "EmployeeContract",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "ContractNumber",
                table: "EmployeeContract",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<long>(
                name: "BudgetLine",
                table: "EmployeeContract",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WorkTime",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Province",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Project",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DutyStation",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DurationOfContract",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Designation",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Country",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ContractPeriod",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ContractNumber",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BudgetLine",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

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

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EmployeeContract_DesignationDetail_Designation",
            //    table: "EmployeeContract",
            //    column: "Designation",
            //    principalTable: "DesignationDetail",
            //    principalColumn: "DesignationId",
            //    onDelete: ReferentialAction.Cascade);

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
    }
}

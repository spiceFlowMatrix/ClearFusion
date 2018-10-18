using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeDetailAfterTruncate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advances_EmployeeDetail_EmployeeId",
                table: "Advances");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaryDetails_EmployeeDetail_EmployeeId",
                table: "EmployeeSalaryDetails");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSalaryDetails_EmployeeId",
                table: "EmployeeSalaryDetails");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeProfessionalDetail_EmployeeId",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropIndex(
                name: "IX_Advances_EmployeeId",
                table: "Advances");

            migrationBuilder.AddColumn<long>(
                name: "AttendanceId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeProfessionalId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoOfChildren",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PayrollId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegCode",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SalaryId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ScheduleId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_EmployeeId",
                table: "EmployeeProfessionalDetail",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_EmployeeProfessionalId",
                table: "EmployeeDetail",
                column: "EmployeeProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_SalaryId",
                table: "EmployeeDetail",
                column: "SalaryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetail_EmployeeProfessionalDetail_EmployeeProfessionalId",
                table: "EmployeeDetail",
                column: "EmployeeProfessionalId",
                principalTable: "EmployeeProfessionalDetail",
                principalColumn: "EmployeeProfessionalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetail_EmployeeSalaryDetails_SalaryId",
                table: "EmployeeDetail",
                column: "SalaryId",
                principalTable: "EmployeeSalaryDetails",
                principalColumn: "SalaryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetail_EmployeeProfessionalDetail_EmployeeProfessionalId",
                table: "EmployeeDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetail_EmployeeSalaryDetails_SalaryId",
                table: "EmployeeDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeProfessionalDetail_EmployeeId",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetail_EmployeeProfessionalId",
                table: "EmployeeDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetail_SalaryId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "AttendanceId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "EmployeeProfessionalId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "NoOfChildren",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "PayrollId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "RegCode",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "SalaryId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "EmployeeDetail");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryDetails_EmployeeId",
                table: "EmployeeSalaryDetails",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_EmployeeId",
                table: "EmployeeProfessionalDetail",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advances_EmployeeId",
                table: "Advances",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Advances_EmployeeDetail_EmployeeId",
                table: "Advances",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaryDetails_EmployeeDetail_EmployeeId",
                table: "EmployeeSalaryDetails",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

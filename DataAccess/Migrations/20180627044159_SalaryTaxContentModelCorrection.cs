using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class SalaryTaxContentModelCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PositionAuthorizedOfficer",
                table: "SalaryTaxReportContent",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "EmployerAuthorizedOfficerName",
                table: "SalaryTaxReportContent",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PositionAuthorizedOfficer",
                table: "SalaryTaxReportContent",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployerAuthorizedOfficerName",
                table: "SalaryTaxReportContent",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addColumnOnEmployeeBonusFineSalaryHeadTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "EmployeeBonusFineSalaryHead",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "EmployeeBonusFineSalaryHead",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "EmployeeBonusFineSalaryHead");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "EmployeeBonusFineSalaryHead");
        }
    }
}

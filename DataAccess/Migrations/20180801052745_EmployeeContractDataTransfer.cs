using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeContractDataTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ContractNumber",
                table: "EmployeeContract",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ContractPeriod",
                table: "EmployeeContract",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "ContractStatus",
                table: "EmployeeContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PeriodType",
                table: "EmployeeContract",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractNumber",
                table: "EmployeeContract");

            migrationBuilder.DropColumn(
                name: "ContractPeriod",
                table: "EmployeeContract");

            migrationBuilder.DropColumn(
                name: "ContractStatus",
                table: "EmployeeContract");

            migrationBuilder.DropColumn(
                name: "PeriodType",
                table: "EmployeeContract");
        }
    }
}

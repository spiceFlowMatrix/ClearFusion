using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddDariFieldsInEmployeeContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryDari",
                table: "EmployeeContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DesignationDari",
                table: "EmployeeContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DutyStationDari",
                table: "EmployeeContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherNameDari",
                table: "EmployeeContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradeDari",
                table: "EmployeeContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobDari",
                table: "EmployeeContract",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceDari",
                table: "EmployeeContract",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryDari",
                table: "EmployeeContract");

            migrationBuilder.DropColumn(
                name: "DesignationDari",
                table: "EmployeeContract");

            migrationBuilder.DropColumn(
                name: "DutyStationDari",
                table: "EmployeeContract");

            migrationBuilder.DropColumn(
                name: "FatherNameDari",
                table: "EmployeeContract");

            migrationBuilder.DropColumn(
                name: "GradeDari",
                table: "EmployeeContract");

            migrationBuilder.DropColumn(
                name: "JobDari",
                table: "EmployeeContract");

            migrationBuilder.DropColumn(
                name: "ProvinceDari",
                table: "EmployeeContract");
        }
    }
}

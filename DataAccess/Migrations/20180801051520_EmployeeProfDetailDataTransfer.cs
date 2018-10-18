using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeProfDetailDataTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Departments",
                table: "EmployeeProfessionalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "EmployeeProfessionalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "EmployeeProfessionalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkType",
                table: "EmployeeProfessionalDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Departments",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropColumn(
                name: "WorkType",
                table: "EmployeeProfessionalDetail");
        }
    }
}

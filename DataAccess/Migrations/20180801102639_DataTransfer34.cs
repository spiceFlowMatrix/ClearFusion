using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DataTransfer34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "EmployeeProfessionalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegCode",
                table: "EmployeeProfessionalDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropColumn(
                name: "RegCode",
                table: "EmployeeProfessionalDetail");
        }
    }
}

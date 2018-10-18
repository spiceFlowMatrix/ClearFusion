using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DataTransferEmployeeEmployeeProfessionalDetailWithForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractStatus",
                table: "EmployeeProfessionalDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractStatus",
                table: "EmployeeProfessionalDetail");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class employeedetailsFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BirthPlace",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuePlace",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaritalStatus",
                table: "EmployeeDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PassportNo",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "EmployeeDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthPlace",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "IssuePlace",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "PassportNo",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "University",
                table: "EmployeeDetail");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeInfoReferencesColumnAddPhoneNoandEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "EmployeeRelativeInfo",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PhoneNo",
                table: "EmployeeRelativeInfo",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "EmployeeInfoReferences",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PhoneNo",
                table: "EmployeeInfoReferences",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "EmployeeRelativeInfo");

            migrationBuilder.DropColumn(
                name: "PhoneNo",
                table: "EmployeeRelativeInfo");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "EmployeeInfoReferences");

            migrationBuilder.DropColumn(
                name: "PhoneNo",
                table: "EmployeeInfoReferences");
        }
    }
}

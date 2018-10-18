using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DataTransfer31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "EmployeeDetail",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CloseRelativeList",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationList",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InternationalEmploymentList",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalEmploymentList",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSkillList",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreeList",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpeakLanguageList",
                table: "EmployeeDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseRelativeList",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "EducationList",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "InternationalEmploymentList",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "NationalEmploymentList",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "OtherSkillList",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "RefreeList",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "SpeakLanguageList",
                table: "EmployeeDetail");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "EmployeeDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}

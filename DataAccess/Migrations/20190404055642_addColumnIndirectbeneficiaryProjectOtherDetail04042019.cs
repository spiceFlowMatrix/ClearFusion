using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addColumnIndirectbeneficiaryProjectOtherDetail04042019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InDirectBeneficiaryFemale",
                table: "ProjectOtherDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InDirectBeneficiaryMale",
                table: "ProjectOtherDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OpportunityType",
                table: "ProjectOtherDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "beneficiaryFemale",
                table: "ProjectOtherDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "beneficiaryMale",
                table: "ProjectOtherDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InDirectBeneficiaryFemale",
                table: "ProjectOtherDetail");

            migrationBuilder.DropColumn(
                name: "InDirectBeneficiaryMale",
                table: "ProjectOtherDetail");

            migrationBuilder.DropColumn(
                name: "OpportunityType",
                table: "ProjectOtherDetail");

            migrationBuilder.DropColumn(
                name: "beneficiaryFemale",
                table: "ProjectOtherDetail");

            migrationBuilder.DropColumn(
                name: "beneficiaryMale",
                table: "ProjectOtherDetail");
        }
    }
}

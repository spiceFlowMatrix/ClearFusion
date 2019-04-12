using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class removeColumnIndirectbeneficiaryProjectOtherDetail04042019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "beneficiaryFemale",
                table: "ProjectOtherDetail");

            migrationBuilder.DropColumn(
                name: "beneficiaryMale",
                table: "ProjectOtherDetail");

            migrationBuilder.RenameColumn(
                name: "opportunitydescription",
                table: "ProjectOtherDetail",
                newName: "Opportunitydescription");

            migrationBuilder.RenameColumn(
                name: "opportunityNo",
                table: "ProjectOtherDetail",
                newName: "OpportunityNo");

            migrationBuilder.RenameColumn(
                name: "opportunity",
                table: "ProjectOtherDetail",
                newName: "Opportunity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Opportunitydescription",
                table: "ProjectOtherDetail",
                newName: "opportunitydescription");

            migrationBuilder.RenameColumn(
                name: "OpportunityNo",
                table: "ProjectOtherDetail",
                newName: "opportunityNo");

            migrationBuilder.RenameColumn(
                name: "Opportunity",
                table: "ProjectOtherDetail",
                newName: "opportunity");

            migrationBuilder.AddColumn<string>(
                name: "beneficiaryFemale",
                table: "ProjectOtherDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "beneficiaryMale",
                table: "ProjectOtherDetail",
                nullable: true);
        }
    }
}

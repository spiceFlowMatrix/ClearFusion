using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class renameColumnopportunityProjectOtherDetail04042019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

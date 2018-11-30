using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addmigrationProposalSupportiveDoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BudgetFileWebLink",
                table: "ProjectProposalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConceptFileWebLink",
                table: "ProjectProposalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EDIFileWebLink",
                table: "ProjectProposalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentationFileWebLink",
                table: "ProjectProposalDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BudgetFileWebLink",
                table: "ProjectProposalDetail");

            migrationBuilder.DropColumn(
                name: "ConceptFileWebLink",
                table: "ProjectProposalDetail");

            migrationBuilder.DropColumn(
                name: "EDIFileWebLink",
                table: "ProjectProposalDetail");

            migrationBuilder.DropColumn(
                name: "PresentationFileWebLink",
                table: "ProjectProposalDetail");
        }
    }
}

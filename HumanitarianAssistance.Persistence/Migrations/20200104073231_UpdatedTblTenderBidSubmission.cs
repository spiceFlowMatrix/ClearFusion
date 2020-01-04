using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class UpdatedTblTenderBidSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBidSubmitted",
                table: "TenderBidSubmission",
                newName: "IsBidSelected");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBidSelected",
                table: "TenderBidSubmission",
                newName: "IsBidSubmitted");
        }
    }
}

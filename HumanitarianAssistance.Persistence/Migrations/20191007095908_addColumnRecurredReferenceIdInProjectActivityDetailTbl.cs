using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addColumnRecurredReferenceIdInProjectActivityDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ReoccuredReferenceId",
                table: "ProjectActivityDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReoccuredReferenceId",
                table: "ProjectActivityDetail");
        }
    }
}

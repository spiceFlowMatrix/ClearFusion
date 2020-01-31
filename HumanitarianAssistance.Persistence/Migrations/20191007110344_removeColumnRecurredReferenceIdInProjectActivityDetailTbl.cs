using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class removeColumnRecurredReferenceIdInProjectActivityDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReoccuredReferenceId",
                table: "ProjectActivityDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ReoccuredReferenceId",
                table: "ProjectActivityDetail",
                nullable: true);
        }
    }
}

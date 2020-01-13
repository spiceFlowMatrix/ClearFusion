using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateColumnInProjectHiringRequestDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractType",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.AddColumn<int>(
                name: "ContractTypeId",
                table: "ProjectHiringRequestDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractTypeId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.AddColumn<string>(
                name: "ContractType",
                table: "ProjectHiringRequestDetail",
                nullable: true);
        }
    }
}

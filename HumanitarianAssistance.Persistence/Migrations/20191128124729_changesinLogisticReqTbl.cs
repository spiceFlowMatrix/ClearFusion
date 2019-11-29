using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class changesinLogisticReqTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestName",
                table: "ProjectLogisticRequests",
                newName: "RequestCode");

            migrationBuilder.RenameColumn(
                name: "EstimatedCost",
                table: "ProjectLogisticItems",
                newName: "EstimatedUnitCost");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProjectLogisticRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProjectLogisticRequests");

            migrationBuilder.RenameColumn(
                name: "RequestCode",
                table: "ProjectLogisticRequests",
                newName: "RequestName");

            migrationBuilder.RenameColumn(
                name: "EstimatedUnitCost",
                table: "ProjectLogisticItems",
                newName: "EstimatedCost");
        }
    }
}

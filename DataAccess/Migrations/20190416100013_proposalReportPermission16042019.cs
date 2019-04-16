using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class proposalReportPermission16042019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationPages",
                columns: new[] { "PageId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ModuleId", "ModuleName", "PageName" },
                values: new object[] { 80, null, null, false, null, null, 8, "Projects", "ProjectBudgetLine" });

            migrationBuilder.InsertData(
                table: "ApplicationPages",
                columns: new[] { "PageId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ModuleId", "ModuleName", "PageName" },
                values: new object[] { 81, null, null, false, null, null, 8, "Projects", "BroadCastPolicy" });

            migrationBuilder.InsertData(
                table: "ApplicationPages",
                columns: new[] { "PageId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ModuleId", "ModuleName", "PageName" },
                values: new object[] { 82, null, null, false, null, null, 8, "Projects", "ProposalReport" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 82);
        }
    }
}

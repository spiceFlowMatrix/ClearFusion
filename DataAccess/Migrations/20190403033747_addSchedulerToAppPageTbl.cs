using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addSchedulerToAppPageTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationPages",
                columns: new[] { "PageId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ModuleId", "ModuleName", "PageName" },
                values: new object[] { 77, null, null, false, null, null, 6, "Marketing", "Scheduler" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 77);
        }
    }
}

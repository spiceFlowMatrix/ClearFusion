using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateApplicationpages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationPages",
                columns: new[] { "PageId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ModuleId", "ModuleName", "PageName" },
                values: new object[] { 74, null, null, false, null, null, 8, "Projects", "ProjectJobs" });

            migrationBuilder.InsertData(
                table: "ApplicationPages",
                columns: new[] { "PageId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ModuleId", "ModuleName", "PageName" },
                values: new object[] { 75, null, null, false, null, null, 8, "Projects", "ProjectActivities" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 75);
        }
    }
}

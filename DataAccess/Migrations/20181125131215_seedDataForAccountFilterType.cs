using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class seedDataForAccountFilterType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AccountFilterType",
                columns: new[] { "AccountFilterTypeId", "AccountFilterTypeName", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate" },
                values: new object[] { 1, "Inventory Account", null, null, false, null, null });

            migrationBuilder.InsertData(
                table: "AccountFilterType",
                columns: new[] { "AccountFilterTypeId", "AccountFilterTypeName", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate" },
                values: new object[] { 2, "Salary Account", null, null, false, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountFilterType",
                keyColumn: "AccountFilterTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AccountFilterType",
                keyColumn: "AccountFilterTypeId",
                keyValue: 2);
        }
    }
}

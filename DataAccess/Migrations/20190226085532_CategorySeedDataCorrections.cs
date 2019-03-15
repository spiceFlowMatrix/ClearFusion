using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class CategorySeedDataCorrections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 1L, "Bank", null, null, false, null, null },
                    { 2L, "NGO", null, null, false, null, null },
                    { 3L, "Telecommunicaton", null, null, false, null, null },
                    { 4L, "Government", null, null, false, null, null },
                    { 5L, "Hospital", null, null, false, null, null },
                    { 6L, "Travel Agency", null, null, false, null, null },
                    { 7L, "University", null, null, false, null, null },
                    { 8L, "Media Groups", null, null, false, null, null },
                    { 9L, "Shops", null, null, false, null, null },
                    { 10L, "Energy", null, null, false, null, null },
                    { 11L, "School", null, null, false, null, null },
                    { 12L, "Construction", null, null, false, null, null }
                });
        }
    }
}

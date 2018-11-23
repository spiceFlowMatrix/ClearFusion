using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class initSeedDataAddedProjectRelated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GenderConsiderationDetail",
                columns: new[] { "GenderConsiderationId", "CreatedById", "CreatedDate", "GenderConsiderationName", "IsDeleted", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 1L, null, null, "50 % F - 50 % M Excellent", false, null, null },
                    { 2L, null, null, "40 % F - 60 % M Very Good", false, null, null },
                    { 3L, null, null, "30 % F - 70 % M Good", false, null, null },
                    { 4L, null, null, "25 % F - 75 % M Poor", false, null, null },
                    { 5L, null, null, "20 % F - 80 % M Poor", false, null, null },
                    { 6L, null, null, "10 % F - 90 % M Poor", false, null, null },
                    { 7L, null, null, "5 % F - 95 % M Poor", false, null, null },
                    { 8L, null, null, "0 % F - 100 % M Poor", false, null, null }
                });

            migrationBuilder.InsertData(
                table: "SecurityConsiderationDetail",
                columns: new[] { "SecurityConsiderationId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "SecurityConsiderationName" },
                values: new object[,]
                {
                    { 11L, null, null, false, null, null, "Future Threats expected" },
                    { 10L, null, null, false, null, null, "No obstacle for deploying Resources & office" },
                    { 9L, null, null, false, null, null, "No barrier for staff to access the area" },
                    { 8L, null, null, false, null, null, "Future Threats exits" },
                    { 7L, null, null, false, null, null, "Resources can be deployed partially" },
                    { 5L, null, null, false, null, null, "Project staff access the are partially" },
                    { 4L, null, null, false, null, null, "Threat exit for future (Highly)" },
                    { 3L, null, null, false, null, null, "Resources cannot be deployed" },
                    { 2L, null, null, false, null, null, "Beneficiaries cannot be reached" },
                    { 1L, null, null, false, null, null, "Project Staff Cannot Visit Project Site" },
                    { 6L, null, null, false, null, null, "Bonfires can be reached partially" }
                });

            migrationBuilder.InsertData(
                table: "SecurityDetail",
                columns: new[] { "SecurityId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "SecurityName" },
                values: new object[,]
                {
                    { 1L, null, null, false, null, null, "Insecure" },
                    { 2L, null, null, false, null, null, "Partially Insecure" },
                    { 3L, null, null, false, null, null, "Secure (Green Area)" }
                });

            migrationBuilder.InsertData(
                table: "StrengthConsiderationDetail",
                columns: new[] { "StrengthConsiderationId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "StrengthConsiderationName" },
                values: new object[,]
                {
                    { 2L, null, null, false, null, null, "Not Gender Friendly" },
                    { 1L, null, null, false, null, null, "Gender Friendly" },
                    { 3L, null, null, false, null, null, "Not Applicable" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GenderConsiderationDetail",
                keyColumn: "GenderConsiderationId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "GenderConsiderationDetail",
                keyColumn: "GenderConsiderationId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "GenderConsiderationDetail",
                keyColumn: "GenderConsiderationId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "GenderConsiderationDetail",
                keyColumn: "GenderConsiderationId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "GenderConsiderationDetail",
                keyColumn: "GenderConsiderationId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "GenderConsiderationDetail",
                keyColumn: "GenderConsiderationId",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "GenderConsiderationDetail",
                keyColumn: "GenderConsiderationId",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "GenderConsiderationDetail",
                keyColumn: "GenderConsiderationId",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "SecurityConsiderationDetail",
                keyColumn: "SecurityConsiderationId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "SecurityConsiderationDetail",
                keyColumn: "SecurityConsiderationId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "SecurityConsiderationDetail",
                keyColumn: "SecurityConsiderationId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "SecurityConsiderationDetail",
                keyColumn: "SecurityConsiderationId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "SecurityConsiderationDetail",
                keyColumn: "SecurityConsiderationId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "SecurityConsiderationDetail",
                keyColumn: "SecurityConsiderationId",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "SecurityConsiderationDetail",
                keyColumn: "SecurityConsiderationId",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "SecurityConsiderationDetail",
                keyColumn: "SecurityConsiderationId",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "SecurityConsiderationDetail",
                keyColumn: "SecurityConsiderationId",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "SecurityConsiderationDetail",
                keyColumn: "SecurityConsiderationId",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "SecurityConsiderationDetail",
                keyColumn: "SecurityConsiderationId",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "SecurityDetail",
                keyColumn: "SecurityId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "SecurityDetail",
                keyColumn: "SecurityId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "SecurityDetail",
                keyColumn: "SecurityId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "StrengthConsiderationDetail",
                keyColumn: "StrengthConsiderationId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "StrengthConsiderationDetail",
                keyColumn: "StrengthConsiderationId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "StrengthConsiderationDetail",
                keyColumn: "StrengthConsiderationId",
                keyValue: 3L);
        }
    }
}

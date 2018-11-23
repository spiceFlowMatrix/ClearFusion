using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class initSeedDataAddedEmployeeRelated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CodeType",
                columns: new[] { "CodeTypeId", "CodeTypeName" },
                values: new object[,]
                {
                    { 1, "Organizations" },
                    { 2, "Suppliers" },
                    { 3, "Repair Shops" },
                    { 4, "Individual/Others" },
                    { 5, "Locations/Stores" }
                });

            migrationBuilder.InsertData(
                table: "LanguageDetail",
                columns: new[] { "LanguageId", "CreatedById", "CreatedDate", "IsDeleted", "LanguageName", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 11, null, null, false, "Uzbek", null, null },
                    { 10, null, null, false, "Urdu", null, null },
                    { 8, null, null, false, "Turkish", null, null },
                    { 7, null, null, false, "Russian", null, null },
                    { 6, null, null, false, "Pashto", null, null },
                    { 9, null, null, false, "Turkmani", null, null },
                    { 4, null, null, false, "French", null, null },
                    { 3, null, null, false, "English", null, null },
                    { 2, null, null, false, "Dari", null, null },
                    { 1, null, null, false, "Arabic", null, null },
                    { 5, null, null, false, "German", null, null }
                });

            migrationBuilder.InsertData(
                table: "SalaryHeadDetails",
                columns: new[] { "SalaryHeadId", "AccountNo", "CreatedById", "CreatedDate", "Description", "HeadName", "HeadTypeId", "IsDeleted", "ModifiedById", "ModifiedDate", "TransactionTypeId" },
                values: new object[,]
                {
                    { 10, null, null, null, "Other2Allowance", "Other2Allowance", 1, false, null, null, 2 },
                    { 1, null, null, null, "Tr Allowance", "Tr Allowance", 1, false, null, null, 2 },
                    { 2, null, null, null, "Food Allowance", "Food Allowance", 1, false, null, null, 2 },
                    { 3, null, null, null, "Fine Deduction", "Fine Deduction", 2, false, null, null, 1 },
                    { 4, null, null, null, "Capacity Building Deduction", "Capacity Building Deduction", 2, false, null, null, 1 },
                    { 5, null, null, null, "Security Deduction", "Security Deduction", 2, false, null, null, 1 },
                    { 6, null, null, null, "Other Allowance", "Other Allowance", 1, false, null, null, 2 },
                    { 7, null, null, null, "Other Deduction", "Other Deduction", 2, false, null, null, 1 },
                    { 8, null, null, null, "Medical Allowance", "Medical Allowance", 1, false, null, null, 2 },
                    { 9, null, null, null, "Other1Allowance", "Other1Allowance", 1, false, null, null, 2 },
                    { 11, null, null, null, "Basic Pay (In hours)", "Basic Pay (In hours)", 3, false, null, null, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CodeType",
                keyColumn: "CodeTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CodeType",
                keyColumn: "CodeTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CodeType",
                keyColumn: "CodeTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CodeType",
                keyColumn: "CodeTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CodeType",
                keyColumn: "CodeTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LanguageDetail",
                keyColumn: "LanguageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LanguageDetail",
                keyColumn: "LanguageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LanguageDetail",
                keyColumn: "LanguageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LanguageDetail",
                keyColumn: "LanguageId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LanguageDetail",
                keyColumn: "LanguageId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LanguageDetail",
                keyColumn: "LanguageId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LanguageDetail",
                keyColumn: "LanguageId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LanguageDetail",
                keyColumn: "LanguageId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "LanguageDetail",
                keyColumn: "LanguageId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "LanguageDetail",
                keyColumn: "LanguageId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "LanguageDetail",
                keyColumn: "LanguageId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SalaryHeadDetails",
                keyColumn: "SalaryHeadId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SalaryHeadDetails",
                keyColumn: "SalaryHeadId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SalaryHeadDetails",
                keyColumn: "SalaryHeadId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SalaryHeadDetails",
                keyColumn: "SalaryHeadId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SalaryHeadDetails",
                keyColumn: "SalaryHeadId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SalaryHeadDetails",
                keyColumn: "SalaryHeadId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SalaryHeadDetails",
                keyColumn: "SalaryHeadId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SalaryHeadDetails",
                keyColumn: "SalaryHeadId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SalaryHeadDetails",
                keyColumn: "SalaryHeadId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SalaryHeadDetails",
                keyColumn: "SalaryHeadId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SalaryHeadDetails",
                keyColumn: "SalaryHeadId",
                keyValue: 11);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class SeedDataForApplicationPages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationPages",
                columns: new[] { "PageId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ModuleId", "ModuleName", "PageName" },
                values: new object[,]
                {
                    { 1, null, null, false, null, null, 1, "Users", "Users" },
                    { 27, null, null, false, null, null, 3, "Accounting", "FinancialReport" },
                    { 28, null, null, false, null, null, 3, "Accounting", "CategoryPopulator" },
                    { 29, null, null, false, null, null, 3, "Accounting", "ExchangeGainLoss" },
                    { 30, null, null, false, null, null, 3, "Accounting", "GainLossTransaction" },
                    { 31, null, null, false, null, null, 3, "Accounting", "PensionPayments" },
                    { 32, null, null, false, null, null, 4, "HR", "Employees" },
                    { 33, null, null, false, null, null, 4, "HR", "PayrollDailyHours" },
                    { 34, null, null, false, null, null, 4, "HR", "Holidays" },
                    { 35, null, null, false, null, null, 4, "HR", "Attendance" },
                    { 26, null, null, false, null, null, 3, "Accounting", "TrialBalance" },
                    { 36, null, null, false, null, null, 4, "HR", "ApproveLeave" },
                    { 38, null, null, false, null, null, 4, "HR", "Jobs" },
                    { 39, null, null, false, null, null, 4, "HR", "Interview" },
                    { 40, null, null, false, null, null, 4, "HR", "EmployeeAppraisal" },
                    { 41, null, null, false, null, null, 4, "HR", "Advances" },
                    { 42, null, null, false, null, null, 4, "HR", "Summary" },
                    { 43, null, null, false, null, null, 5, "Store", "Categories" },
                    { 44, null, null, false, null, null, 5, "Store", "StoreSourceCodes" },
                    { 45, null, null, false, null, null, 5, "Store", "PaymentTypes" },
                    { 46, null, null, false, null, null, 5, "Store", "Store" },
                    { 37, null, null, false, null, null, 4, "HR", "MonthlyPayrollRegister" },
                    { 25, null, null, false, null, null, 3, "Accounting", "BudgetBalance" },
                    { 24, null, null, false, null, null, 3, "Accounting", "LedgerStatement" },
                    { 23, null, null, false, null, null, 3, "Accounting", "Journal" },
                    { 2, null, null, false, null, null, 2, "Code", "ChartOfAccount" },
                    { 3, null, null, false, null, null, 2, "Code", "JournalCodes" },
                    { 4, null, null, false, null, null, 2, "Code", "CurrencyCodes" },
                    { 5, null, null, false, null, null, 2, "Code", "OfficeCodes" },
                    { 6, null, null, false, null, null, 2, "Code", "FinancialYear" },
                    { 7, null, null, false, null, null, 2, "Code", "PensionRate" },
                    { 8, null, null, false, null, null, 2, "Code", "EmployeeContract" },
                    { 9, null, null, false, null, null, 2, "Code", "AppraisalQuestions" },
                    { 10, null, null, false, null, null, 2, "Code", "TechnicalQuestions" },
                    { 11, null, null, false, null, null, 2, "Code", "EmailSettings" },
                    { 12, null, null, false, null, null, 2, "Code", "ExchangeRate" },
                    { 13, null, null, false, null, null, 2, "Code", "LeaveReason" },
                    { 14, null, null, false, null, null, 2, "Code", "Profession" },
                    { 15, null, null, false, null, null, 2, "Code", "Department" },
                    { 16, null, null, false, null, null, 2, "Code", "Qualification" },
                    { 17, null, null, false, null, null, 2, "Code", "Designation" },
                    { 18, null, null, false, null, null, 2, "Code", "JobGrade" },
                    { 19, null, null, false, null, null, 2, "Code", "SalaryHead" },
                    { 20, null, null, false, null, null, 2, "Code", "SalaryTaxReportContent" },
                    { 21, null, null, false, null, null, 2, "Code", "SetPayrollAccount" },
                    { 22, null, null, false, null, null, 3, "Accounting", "Vouchers" },
                    { 47, null, null, false, null, null, 5, "Store", "ProcurementSummary" },
                    { 48, null, null, false, null, null, 5, "Store", "DepreciationReport" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 48);
        }
    }
}

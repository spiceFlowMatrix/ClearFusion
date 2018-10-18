using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DataTransfer13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDocumentDetail_EmployeeDetail_EmployeeID",
                table: "EmployeeDocumentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetail_CurrencyDetails_CurrencyId",
                table: "VoucherDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactionDetails_VoucherDetail_VoucherNo",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "RowValidated",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "StoreItemCode",
                table: "VoucherTransactionDetails");

            migrationBuilder.RenameColumn(
                name: "Dr",
                table: "VoucherTransactionDetails",
                newName: "Debit");

            migrationBuilder.RenameColumn(
                name: "Cr",
                table: "VoucherTransactionDetails",
                newName: "Credit");

            migrationBuilder.AlterColumn<long>(
                name: "VoucherNo",
                table: "VoucherTransactionDetails",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VoucherTransactionDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VoucherTransactionDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VoucherDocumentDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VoucherDocumentDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "DocumentFilePath",
                table: "VoucherDocumentDetail",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VoucherDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "VoucherDetail",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VoucherDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "VoucherDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficeCode",
                table: "VoucherDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoucherMode",
                table: "VoucherDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoucherType",
                table: "VoucherDetail",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleMileages",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleLocations",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VehicleFuel",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleFuel",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "UserDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "UserDetailOffices",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "UserDetailOffices",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TechnicalQuestion",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TechnicalQuestion",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TaskMaster",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TaskMaster",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StrongandWeakPoints",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "StrongandWeakPoints",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StorePurchaseOrders",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "StorePurchaseOrders",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StoreItemPurchases",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "StoreItemPurchases",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StoreInventories",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "StoreInventories",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StatusAtTimeOfIssue",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "StatusAtTimeOfIssue",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "SalaryTaxReportContent",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "SalaryTaxReportContent",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "SalaryHeadDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "SalaryHeadDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ReceiptType",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ReceiptType",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "RatingBasedCriteria",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "RatingBasedCriteria",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "QualificationDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "QualificationDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PurchaseVehicles",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PurchaseVehicles",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PurchaseUnitType",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PurchaseUnitType",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PurchaseGenerators",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PurchaseGenerators",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProvinceDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProvinceDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectDocument",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectDocument",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectBudgetLine",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectBudgetLine",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectBudget",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectBudget",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProfessionDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProfessionDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "ProfessionDari",
                table: "ProfessionDetails",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PermissionsInRoles",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PermissionsInRoles",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PayrollMonthlyHourDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PayrollMonthlyHourDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "OfficeDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "OfficeDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "NotesMaster",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "NotesMaster",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "NationalityDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "NationalityDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MotorSpareParts",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "MotorSpareParts",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MotorMaintenances",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "MotorMaintenances",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "LoggerDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "LoggerDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "LeaveReasonDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "LeaveReasonDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "JournalDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "JournalDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "JobHiringDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "JobHiringDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "JobGrade",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "JobGrade",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ItemSpecificationMaster",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ItemSpecificationMaster",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ItemSpecificationDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ItemSpecificationDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ItemPurchaseDocuments",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ItemPurchaseDocuments",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InventoryItemType",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InventoryItemType",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InventoryItems",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InventoryItems",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewTrainings",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InterviewTrainings",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewTechnicalQuestions",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InterviewTechnicalQuestions",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewTechnicalQuestion",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InterviewTechnicalQuestion",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewScheduleDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InterviewScheduleDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewLanguages",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InterviewLanguages",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InterviewDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "HolidayWeeklyDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "HolidayWeeklyDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "HolidayDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "HolidayDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "FinancialYearDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "FinancialYearDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ExistInterviewDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ExistInterviewDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<double>(
                name: "Rate",
                table: "ExchangeRates",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ExchangeRates",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ExchangeRates",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ExchangeRates",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "ExchangeRates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficeCode",
                table: "ExchangeRates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "ExchangeRates",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeType",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeType",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeSalaryDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeSalaryDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeSalaryBudget",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeSalaryBudget",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeSalaryAnalyticalInfo",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeSalaryAnalyticalInfo",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeRelativeInfo",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeRelativeInfo",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeProfessionalDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeProfessionalDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePensionRate",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeePensionRate",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePayrollMonth",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeePayrollMonth",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePayrollForMonth",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeePayrollForMonth",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePayroll",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeePayroll",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePaymentTypes",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeePaymentTypes",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeOtherSkills",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeOtherSkills",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeInfoReferences",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeInfoReferences",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHistoryOutsideCountry",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeHistoryOutsideCountry",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHistoryDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeHistoryDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHealthQuestion",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeHealthQuestion",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHealthInfo",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeHealthInfo",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeEvaluationTraining",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeEvaluationTraining",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeEvaluation",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeEvaluation",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeEducations",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeEducations",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeDocumentDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "EmployeeDocumentDetail",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeDocumentDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "DocumentFilePath",
                table: "EmployeeDocumentDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "EmployeeDocumentDetail",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeContract",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeContract",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAttendance",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeAttendance",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAppraisalTeamMember",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeAppraisalTeamMember",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAppraisalQuestions",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeAppraisalQuestions",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAppraisalDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeAppraisalDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeApplyLeave",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeApplyLeave",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAnalyticalDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeAnalyticalDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmailType",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmailType",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmailSettingDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmailSettingDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DistrictDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DistrictDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DesignationDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DesignationDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "DesignationDari",
                table: "DesignationDetail",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Department",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Department",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "CurrencyDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CurrencyDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CurrencyDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CountryDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CountryDetails",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ContractTypeContent",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ContractTypeContent",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ChartAccountDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ChartAccountDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<int>(
                name: "AccountNote",
                table: "ChartAccountDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepMethod",
                table: "ChartAccountDetail",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "DepRate",
                table: "ChartAccountDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MDCode",
                table: "ChartAccountDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Show",
                table: "ChartAccountDetail",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CategoryPopulator",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CategoryPopulator",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetReceivedAmount",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BudgetReceivedAmount",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetReceivable",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BudgetReceivable",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetPayableAmount",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BudgetPayableAmount",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetPayable",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BudgetPayable",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetLineType",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BudgetLineType",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetLineEmployees",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BudgetLineEmployees",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AssignLeaveToEmployee",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AssignLeaveToEmployee",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AssignActivityFeedback",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AssignActivityFeedback",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AssignActivity",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AssignActivity",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AppraisalGeneralQuestions",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppraisalGeneralQuestions",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<float>(
                name: "ReceivedAmount",
                table: "AnalyticalDetail",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AnalyticalDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AnalyticalDetail",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<float>(
                name: "BLAmount",
                table: "AnalyticalDetail",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Advances",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Advances",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ActivityMaster",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ActivityMaster",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "AccountCategory",
                table: "AccountType",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AccountNote",
                table: "AccountType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BalanceType",
                table: "AccountType",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDocumentDetail_EmployeeDetail_EmployeeID",
                table: "EmployeeDocumentDetail",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetail_CurrencyDetails_CurrencyId",
                table: "VoucherDetail",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactionDetails_VoucherDetail_VoucherNo",
                table: "VoucherTransactionDetails",
                column: "VoucherNo",
                principalTable: "VoucherDetail",
                principalColumn: "VoucherNo",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDocumentDetail_EmployeeDetail_EmployeeID",
                table: "EmployeeDocumentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetail_CurrencyDetails_CurrencyId",
                table: "VoucherDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactionDetails_VoucherDetail_VoucherNo",
                table: "VoucherTransactionDetails");

            migrationBuilder.DropColumn(
                name: "DocumentFilePath",
                table: "VoucherDocumentDetail");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "VoucherDetail");

            migrationBuilder.DropColumn(
                name: "OfficeCode",
                table: "VoucherDetail");

            migrationBuilder.DropColumn(
                name: "VoucherMode",
                table: "VoucherDetail");

            migrationBuilder.DropColumn(
                name: "VoucherType",
                table: "VoucherDetail");

            migrationBuilder.DropColumn(
                name: "ProfessionDari",
                table: "ProfessionDetails");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "OfficeCode",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "DocumentFilePath",
                table: "EmployeeDocumentDetail");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "EmployeeDocumentDetail");

            migrationBuilder.DropColumn(
                name: "DesignationDari",
                table: "DesignationDetail");

            migrationBuilder.DropColumn(
                name: "AccountNote",
                table: "ChartAccountDetail");

            migrationBuilder.DropColumn(
                name: "DepMethod",
                table: "ChartAccountDetail");

            migrationBuilder.DropColumn(
                name: "DepRate",
                table: "ChartAccountDetail");

            migrationBuilder.DropColumn(
                name: "MDCode",
                table: "ChartAccountDetail");

            migrationBuilder.DropColumn(
                name: "Show",
                table: "ChartAccountDetail");

            migrationBuilder.DropColumn(
                name: "AccountNote",
                table: "AccountType");

            migrationBuilder.DropColumn(
                name: "BalanceType",
                table: "AccountType");

            migrationBuilder.RenameColumn(
                name: "Debit",
                table: "VoucherTransactionDetails",
                newName: "Dr");

            migrationBuilder.RenameColumn(
                name: "Credit",
                table: "VoucherTransactionDetails",
                newName: "Cr");

            migrationBuilder.AlterColumn<long>(
                name: "VoucherNo",
                table: "VoucherTransactionDetails",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VoucherTransactionDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VoucherTransactionDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RowValidated",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreItemCode",
                table: "VoucherTransactionDetails",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VoucherDocumentDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VoucherDocumentDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VoucherDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "VoucherDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VoucherDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleMileages",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleLocations",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VehicleFuel",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "VehicleFuel",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "UserDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "UserDetailOffices",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "UserDetailOffices",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TechnicalQuestion",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TechnicalQuestion",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TaskMaster",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TaskMaster",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StrongandWeakPoints",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "StrongandWeakPoints",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StorePurchaseOrders",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "StorePurchaseOrders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StoreItemPurchases",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "StoreItemPurchases",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StoreInventories",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "StoreInventories",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StatusAtTimeOfIssue",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "StatusAtTimeOfIssue",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "SalaryTaxReportContent",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "SalaryTaxReportContent",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "SalaryHeadDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "SalaryHeadDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ReceiptType",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ReceiptType",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "RatingBasedCriteria",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "RatingBasedCriteria",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "QualificationDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "QualificationDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PurchaseVehicles",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PurchaseVehicles",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PurchaseUnitType",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PurchaseUnitType",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PurchaseGenerators",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PurchaseGenerators",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProvinceDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProvinceDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectDocument",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectDocument",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectBudgetLine",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectBudgetLine",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectBudget",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectBudget",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProfessionDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ProfessionDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PermissionsInRoles",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PermissionsInRoles",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PayrollMonthlyHourDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "PayrollMonthlyHourDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "OfficeDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "OfficeDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "NotesMaster",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "NotesMaster",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "NationalityDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "NationalityDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MotorSpareParts",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "MotorSpareParts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MotorMaintenances",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "MotorMaintenances",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "LoggerDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "LoggerDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "LeaveReasonDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "LeaveReasonDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "JournalDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "JournalDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "JobHiringDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "JobHiringDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "JobGrade",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "JobGrade",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ItemSpecificationMaster",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ItemSpecificationMaster",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ItemSpecificationDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ItemSpecificationDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ItemPurchaseDocuments",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ItemPurchaseDocuments",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InventoryItemType",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InventoryItemType",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InventoryItems",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InventoryItems",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewTrainings",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InterviewTrainings",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewTechnicalQuestions",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InterviewTechnicalQuestions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewTechnicalQuestion",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InterviewTechnicalQuestion",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewScheduleDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InterviewScheduleDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewLanguages",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InterviewLanguages",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "InterviewDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "HolidayWeeklyDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "HolidayWeeklyDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "HolidayDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "HolidayDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "FinancialYearDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "FinancialYearDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ExistInterviewDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ExistInterviewDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Rate",
                table: "ExchangeRates",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ExchangeRates",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ExchangeRates",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ExchangeRates",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeType",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeType",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeSalaryDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeSalaryDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeSalaryBudget",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeSalaryBudget",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeSalaryAnalyticalInfo",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeSalaryAnalyticalInfo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeRelativeInfo",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeRelativeInfo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeProfessionalDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeProfessionalDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePensionRate",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeePensionRate",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePayrollMonth",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeePayrollMonth",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePayrollForMonth",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeePayrollForMonth",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePayroll",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeePayroll",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePaymentTypes",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeePaymentTypes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeOtherSkills",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeOtherSkills",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeInfoReferences",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeInfoReferences",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHistoryOutsideCountry",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeHistoryOutsideCountry",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHistoryDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeHistoryDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHealthQuestion",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeHealthQuestion",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHealthInfo",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeHealthInfo",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeEvaluationTraining",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeEvaluationTraining",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeEvaluation",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeEvaluation",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeEducations",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeEducations",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeDocumentDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "EmployeeDocumentDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeDocumentDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAttendance",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeAttendance",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAppraisalTeamMember",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeAppraisalTeamMember",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAppraisalQuestions",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeAppraisalQuestions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAppraisalDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeAppraisalDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeApplyLeave",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeApplyLeave",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAnalyticalDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmployeeAnalyticalDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmailType",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmailType",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmailSettingDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "EmailSettingDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DistrictDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DistrictDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DesignationDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "DesignationDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Department",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Department",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "CurrencyDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CurrencyDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CurrencyDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CountryDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CountryDetails",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ContractTypeContent",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ContractTypeContent",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ChartAccountDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ChartAccountDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CategoryPopulator",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "CategoryPopulator",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetReceivedAmount",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BudgetReceivedAmount",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetReceivable",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BudgetReceivable",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetPayableAmount",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BudgetPayableAmount",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetPayable",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BudgetPayable",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetLineType",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BudgetLineType",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetLineEmployees",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "BudgetLineEmployees",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AssignLeaveToEmployee",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AssignLeaveToEmployee",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AssignActivityFeedback",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AssignActivityFeedback",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AssignActivity",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AssignActivity",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AppraisalGeneralQuestions",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AppraisalGeneralQuestions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ReceivedAmount",
                table: "AnalyticalDetail",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AnalyticalDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AnalyticalDetail",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "BLAmount",
                table: "AnalyticalDetail",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Advances",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Advances",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ActivityMaster",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "ActivityMaster",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountCategory",
                table: "AccountType",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDocumentDetail_EmployeeDetail_EmployeeID",
                table: "EmployeeDocumentDetail",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetail_CurrencyDetails_CurrencyId",
                table: "VoucherDetail",
                column: "CurrencyId",
                principalTable: "CurrencyDetails",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactionDetails_VoucherDetail_VoucherNo",
                table: "VoucherTransactionDetails",
                column: "VoucherNo",
                principalTable: "VoucherDetail",
                principalColumn: "VoucherNo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

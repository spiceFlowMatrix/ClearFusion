using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DataTransfer3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VoucherTransactionDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VoucherDocumentDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VoucherDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VehicleFuel",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "UserDetailOffices",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TechnicalQuestion",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TaskMaster",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StrongandWeakPoints",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StorePurchaseOrders",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StoreItemPurchases",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StoreInventories",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StatusAtTimeOfIssue",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "SalaryTaxReportContent",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "SalaryHeadDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ReceiptType",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "RatingBasedCriteria",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "QualificationDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PurchaseVehicles",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PurchaseUnitType",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PurchaseGenerators",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProvinceDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectDocument",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectBudgetLine",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectBudget",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProfessionDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PermissionsInRoles",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PayrollMonthlyHourDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "OfficeDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "NotesMaster",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "NationalityDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MotorSpareParts",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MotorMaintenances",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "LoggerDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "LeaveReasonDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "JournalDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "JobHiringDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "JobGrade",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ItemSpecificationMaster",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ItemSpecificationDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ItemPurchaseDocuments",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InventoryItemType",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InventoryItems",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewTrainings",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewTechnicalQuestions",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewTechnicalQuestion",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewScheduleDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewLanguages",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "HolidayWeeklyDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "HolidayDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "FinancialYearDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ExistInterviewDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ExchangeRates",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeType",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeSalaryDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeSalaryBudget",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeSalaryAnalyticalInfo",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeRelativeInfo",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeProfessionalDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePensionRate",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePayrollMonth",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePayrollForMonth",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePayroll",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePaymentTypes",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeOtherSkills",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeInfoReferences",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHistoryOutsideCountry",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHistoryDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHealthQuestion",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHealthInfo",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeEvaluationTraining",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeEvaluation",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeEducations",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeDocumentDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeContract",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAttendance",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAppraisalTeamMember",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAppraisalQuestions",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAppraisalDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeApplyLeave",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAnalyticalDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmailType",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmailSettingDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DistrictDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DesignationDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Department",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CurrencyDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CountryDetails",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ContractTypeContent",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ChartAccountDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CategoryPopulator",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetReceivedAmount",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetReceivable",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetPayableAmount",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetPayable",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetLineType",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetLineEmployees",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AssignLeaveToEmployee",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AssignActivityFeedback",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AssignActivity",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AppraisalGeneralQuestions",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AnalyticalDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Advances",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ActivityMaster",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VoucherTransactionDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VoucherDocumentDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VoucherDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "VehicleFuel",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "UserDetailOffices",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TechnicalQuestion",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "TaskMaster",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StrongandWeakPoints",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StorePurchaseOrders",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StoreItemPurchases",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StoreInventories",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "StatusAtTimeOfIssue",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "SalaryTaxReportContent",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "SalaryHeadDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ReceiptType",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "RatingBasedCriteria",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "QualificationDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PurchaseVehicles",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PurchaseUnitType",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PurchaseGenerators",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProvinceDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectDocument",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectBudgetLine",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProjectBudget",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ProfessionDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PermissionsInRoles",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PayrollMonthlyHourDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "OfficeDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "NotesMaster",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "NationalityDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MotorSpareParts",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MotorMaintenances",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "LoggerDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "LeaveReasonDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "JournalDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "JobHiringDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "JobGrade",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ItemSpecificationMaster",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ItemSpecificationDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ItemPurchaseDocuments",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InventoryItemType",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InventoryItems",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewTrainings",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewTechnicalQuestions",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewTechnicalQuestion",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewScheduleDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewLanguages",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterviewDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "HolidayWeeklyDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "HolidayDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "FinancialYearDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ExistInterviewDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ExchangeRates",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeType",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeSalaryDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeSalaryBudget",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeSalaryAnalyticalInfo",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeRelativeInfo",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeProfessionalDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePensionRate",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePayrollMonth",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePayrollForMonth",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePayroll",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeePaymentTypes",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeOtherSkills",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeInfoReferences",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHistoryOutsideCountry",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHistoryDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHealthQuestion",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeHealthInfo",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeEvaluationTraining",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeEvaluation",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeEducations",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeDocumentDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeContract",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAttendance",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAppraisalTeamMember",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAppraisalQuestions",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAppraisalDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeApplyLeave",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmployeeAnalyticalDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmailType",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EmailSettingDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DistrictDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DesignationDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Department",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CurrencyDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CountryDetails",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ContractTypeContent",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ChartAccountDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CategoryPopulator",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetReceivedAmount",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetReceivable",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetPayableAmount",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetPayable",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetLineType",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BudgetLineEmployees",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AssignLeaveToEmployee",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AssignActivityFeedback",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AssignActivity",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AppraisalGeneralQuestions",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "AnalyticalDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Advances",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ActivityMaster",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}

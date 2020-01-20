(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["default~accounting-accounting-module~chart-of-accounts-chart-of-accounts-module~configuration-config~44f1780d"],{

/***/ "./src/app/shared/applicationpagesenum.ts":
/*!************************************************!*\
  !*** ./src/app/shared/applicationpagesenum.ts ***!
  \************************************************/
/*! exports provided: ApplicationPages, ApplicationModule, projectPagesMaster, marketingPagesMaster, accountingNewMaster, configurationPagesMaster, HRPagesMaster, StorePagesMaster */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ApplicationPages", function() { return ApplicationPages; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ApplicationModule", function() { return ApplicationModule; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "projectPagesMaster", function() { return projectPagesMaster; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "marketingPagesMaster", function() { return marketingPagesMaster; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "accountingNewMaster", function() { return accountingNewMaster; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "configurationPagesMaster", function() { return configurationPagesMaster; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HRPagesMaster", function() { return HRPagesMaster; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "StorePagesMaster", function() { return StorePagesMaster; });
var ApplicationPages;
(function (ApplicationPages) {
    ApplicationPages[ApplicationPages["ExchangeRates"] = 12] = "ExchangeRates";
    ApplicationPages[ApplicationPages["TimeCategory"] = 49] = "TimeCategory";
    ApplicationPages[ApplicationPages["Quality"] = 50] = "Quality";
    ApplicationPages[ApplicationPages["Phase"] = 51] = "Phase";
    ApplicationPages[ApplicationPages["Nature"] = 52] = "Nature";
    ApplicationPages[ApplicationPages["Medium"] = 53] = "Medium";
    ApplicationPages[ApplicationPages["MediaCategory"] = 54] = "MediaCategory";
    ApplicationPages[ApplicationPages["ActivityType"] = 55] = "ActivityType";
    ApplicationPages[ApplicationPages["Assets"] = 56] = "Assets";
    ApplicationPages[ApplicationPages["Liabilities"] = 57] = "Liabilities";
    ApplicationPages[ApplicationPages["Income"] = 58] = "Income";
    ApplicationPages[ApplicationPages["Expense"] = 59] = "Expense";
    ApplicationPages[ApplicationPages["BalanceSheet"] = 60] = "BalanceSheet";
    ApplicationPages[ApplicationPages["IncomeExpenseReport"] = 61] = "IncomeExpenseReport";
    ApplicationPages[ApplicationPages["ExchangeGainLoss"] = 29] = "ExchangeGainLoss";
    ApplicationPages[ApplicationPages["Vouchers"] = 62] = "Vouchers";
    ApplicationPages[ApplicationPages["Clients"] = 63] = "Clients";
    ApplicationPages[ApplicationPages["UnitRates"] = 64] = "UnitRates";
    ApplicationPages[ApplicationPages["Jobs"] = 38] = "Jobs";
    ApplicationPages[ApplicationPages["Contracts"] = 66] = "Contracts";
    ApplicationPages[ApplicationPages["MyProjects"] = 67] = "MyProjects";
    ApplicationPages[ApplicationPages["Donors"] = 68] = "Donors";
    ApplicationPages[ApplicationPages["ProjectDetails"] = 69] = "ProjectDetails";
    ApplicationPages[ApplicationPages["Proposal"] = 70] = "Proposal";
    ApplicationPages[ApplicationPages["CriteriaEvaluation"] = 71] = "CriteriaEvaluation";
    ApplicationPages[ApplicationPages["Producer"] = 72] = "Producer";
    ApplicationPages[ApplicationPages["Policy"] = 73] = "Policy";
    ApplicationPages[ApplicationPages["ProjectJobs"] = 74] = "ProjectJobs";
    ApplicationPages[ApplicationPages["ProjectActivities"] = 75] = "ProjectActivities";
    ApplicationPages[ApplicationPages["Channel"] = 76] = "Channel";
    ApplicationPages[ApplicationPages["Scheduler"] = 77] = "Scheduler";
    ApplicationPages[ApplicationPages["ProjectDashboard"] = 78] = "ProjectDashboard";
    ApplicationPages[ApplicationPages["ProjectCashFlow"] = 79] = "ProjectCashFlow";
    ApplicationPages[ApplicationPages["ProjectBudgetLine"] = 80] = "ProjectBudgetLine";
    ApplicationPages[ApplicationPages["BroadCastPolicy"] = 81] = "BroadCastPolicy";
    ApplicationPages[ApplicationPages["ProposalReport"] = 82] = "ProposalReport";
    ApplicationPages[ApplicationPages["Users"] = 1] = "Users";
    ApplicationPages[ApplicationPages["ChartOfAccount"] = 2] = "ChartOfAccount";
    ApplicationPages[ApplicationPages["JournalCodes"] = 3] = "JournalCodes";
    ApplicationPages[ApplicationPages["CurrencyCodes"] = 4] = "CurrencyCodes";
    ApplicationPages[ApplicationPages["OfficeCodes"] = 5] = "OfficeCodes";
    ApplicationPages[ApplicationPages["FinancialYear"] = 6] = "FinancialYear";
    ApplicationPages[ApplicationPages["PensionRate"] = 7] = "PensionRate";
    ApplicationPages[ApplicationPages["EmployeeContract"] = 8] = "EmployeeContract";
    ApplicationPages[ApplicationPages["AppraisalQuestions"] = 9] = "AppraisalQuestions";
    ApplicationPages[ApplicationPages["TechnicalQuestions"] = 10] = "TechnicalQuestions";
    ApplicationPages[ApplicationPages["EmailSettings"] = 11] = "EmailSettings";
    ApplicationPages[ApplicationPages["LeaveReason"] = 13] = "LeaveReason";
    ApplicationPages[ApplicationPages["Profession"] = 14] = "Profession";
    ApplicationPages[ApplicationPages["Department"] = 15] = "Department";
    ApplicationPages[ApplicationPages["Qualification"] = 16] = "Qualification";
    ApplicationPages[ApplicationPages["Designation"] = 17] = "Designation";
    ApplicationPages[ApplicationPages["JobGrade"] = 18] = "JobGrade";
    ApplicationPages[ApplicationPages["SalaryHead"] = 19] = "SalaryHead";
    ApplicationPages[ApplicationPages["SalaryTaxReportContent"] = 20] = "SalaryTaxReportContent";
    ApplicationPages[ApplicationPages["SetPayrollAccount"] = 21] = "SetPayrollAccount";
    ApplicationPages[ApplicationPages["Journal"] = 23] = "Journal";
    ApplicationPages[ApplicationPages["LedgerStatement"] = 24] = "LedgerStatement";
    ApplicationPages[ApplicationPages["BudgetBalance"] = 25] = "BudgetBalance";
    ApplicationPages[ApplicationPages["TrialBalance"] = 26] = "TrialBalance";
    ApplicationPages[ApplicationPages["FinancialReport"] = 27] = "FinancialReport";
    ApplicationPages[ApplicationPages["CategoryPopulator"] = 28] = "CategoryPopulator";
    ApplicationPages[ApplicationPages["GainLossTransaction"] = 30] = "GainLossTransaction";
    ApplicationPages[ApplicationPages["PensionPayments"] = 31] = "PensionPayments";
    ApplicationPages[ApplicationPages["Employees"] = 32] = "Employees";
    ApplicationPages[ApplicationPages["PayrollDailyHours"] = 33] = "PayrollDailyHours";
    ApplicationPages[ApplicationPages["Holidays"] = 34] = "Holidays";
    ApplicationPages[ApplicationPages["Attendance"] = 35] = "Attendance";
    ApplicationPages[ApplicationPages["ApproveLeave"] = 36] = "ApproveLeave";
    ApplicationPages[ApplicationPages["MonthlyPayrollRegister"] = 37] = "MonthlyPayrollRegister";
    ApplicationPages[ApplicationPages["Interview"] = 39] = "Interview";
    ApplicationPages[ApplicationPages["EmployeeAppraisal"] = 40] = "EmployeeAppraisal";
    ApplicationPages[ApplicationPages["Advances"] = 41] = "Advances";
    ApplicationPages[ApplicationPages["Summary"] = 42] = "Summary";
    ApplicationPages[ApplicationPages["Categories"] = 43] = "Categories";
    ApplicationPages[ApplicationPages["StoreSourceCodes"] = 44] = "StoreSourceCodes";
    ApplicationPages[ApplicationPages["PaymentTypes"] = 45] = "PaymentTypes";
    ApplicationPages[ApplicationPages["Store"] = 46] = "Store";
    ApplicationPages[ApplicationPages["ProcurementSummary"] = 47] = "ProcurementSummary";
    ApplicationPages[ApplicationPages["DepreciationReport"] = 48] = "DepreciationReport";
    ApplicationPages[ApplicationPages["MarketingJobs"] = 65] = "MarketingJobs";
    ApplicationPages[ApplicationPages["ProjectIndicators"] = 83] = "ProjectIndicators";
    ApplicationPages[ApplicationPages["ProjectPeople"] = 84] = "ProjectPeople";
    ApplicationPages[ApplicationPages["VoucherSummaryreport"] = 85] = "VoucherSummaryreport";
    ApplicationPages[ApplicationPages["HiringRequests"] = 86] = "HiringRequests";
    ApplicationPages[ApplicationPages["RatingQuestions"] = 87] = "RatingQuestions";
})(ApplicationPages || (ApplicationPages = {}));
var ApplicationModule;
(function (ApplicationModule) {
    ApplicationModule[ApplicationModule["Marketing"] = 6] = "Marketing";
    ApplicationModule[ApplicationModule["AccountingNew"] = 7] = "AccountingNew";
    ApplicationModule[ApplicationModule["Projects"] = 8] = "Projects";
})(ApplicationModule || (ApplicationModule = {}));
var projectPagesMaster = {
    ProjectJobs: ApplicationPages.ProjectJobs,
    ProjectActivities: ApplicationPages.ProjectActivities,
    MyProjects: ApplicationPages.MyProjects,
    Donors: ApplicationPages.Donors,
    ProjectDetails: ApplicationPages.ProjectDetails,
    Proposal: ApplicationPages.Proposal,
    CriteriaEvaluation: ApplicationPages.CriteriaEvaluation,
    ProjectDashboard: ApplicationPages.ProjectDashboard,
    ProjectCashFlow: ApplicationPages.ProjectCashFlow,
    ProjectBudgetLine: ApplicationPages.ProjectBudgetLine,
    ProposalReport: ApplicationPages.ProposalReport,
    ProjectIndicators: ApplicationPages.ProjectIndicators,
    ProjectPeople: ApplicationPages.ProjectPeople,
    HiringRequests: ApplicationPages.HiringRequests
};
var marketingPagesMaster = {
    TimeCategory: ApplicationPages.TimeCategory,
    Quality: ApplicationPages.Quality,
    Phase: ApplicationPages.Phase,
    Nature: ApplicationPages.Nature,
    Medium: ApplicationPages.Medium,
    MediaCategory: ApplicationPages.MediaCategory,
    ActivityType: ApplicationPages.ActivityType,
    Clients: ApplicationPages.Clients,
    UnitRates: ApplicationPages.UnitRates,
    Jobs: ApplicationPages.Jobs,
    Contracts: ApplicationPages.Contracts,
    Scheduler: ApplicationPages.Scheduler,
    Producer: ApplicationPages.Producer,
    Channels: ApplicationPages.Channel,
    BroadCastPolicy: ApplicationPages.BroadCastPolicy
};
var accountingNewMaster = {
    Assets: ApplicationPages.Assets,
    Liabilities: ApplicationPages.Liabilities,
    Income: ApplicationPages.Income,
    Expense: ApplicationPages.Expense,
    BalanceSheet: ApplicationPages.BalanceSheet,
    IncomeExpenseReport: ApplicationPages.IncomeExpenseReport,
    Vouchers: ApplicationPages.Vouchers,
    ExchangeRates: ApplicationPages.ExchangeRates,
    ExchangeGainLoss: ApplicationPages.ExchangeGainLoss,
    VoucherSummaryReport: ApplicationPages.VoucherSummaryreport
};
var configurationPagesMaster = {
    JournalCodes: ApplicationPages.JournalCodes,
    CurrencyCodes: ApplicationPages.CurrencyCodes,
    OfficeCodes: ApplicationPages.OfficeCodes,
    FinancialYear: ApplicationPages.FinancialYear,
    PensionRate: ApplicationPages.PensionRate,
    Proposal: ApplicationPages.Proposal,
    EmployeeContract: ApplicationPages.EmployeeContract,
    AppraisalQuestions: ApplicationPages.EmployeeContract,
    TechnicalQuestions: ApplicationPages.TechnicalQuestions,
    EmailSettings: ApplicationPages.EmailSettings,
    ProposalReport: ApplicationPages.ProposalReport,
    LeaveType: ApplicationPages.LeaveReason,
    Profession: ApplicationPages.Profession,
    Department: ApplicationPages.Department,
    Qualification: ApplicationPages.Qualification,
    Designation: ApplicationPages.Designation,
    JobGrade: ApplicationPages.JobGrade,
    SalaryHead: ApplicationPages.SalaryHead,
    SalaryTaxReportContent: ApplicationPages.SalaryTaxReportContent,
    SetPayrollAccount: ApplicationPages.SetPayrollAccount
};
var HRPagesMaster = {
    Employees: ApplicationPages.Employees,
    PayrollDailyHours: ApplicationPages.PayrollDailyHours,
    Holidays: ApplicationPages.Holidays,
    Attendance: ApplicationPages.Attendance,
    ApproveLeave: ApplicationPages.ApproveLeave,
    MonthlyPayrollRegister: ApplicationPages.MonthlyPayrollRegister,
    Jobs: ApplicationPages.Jobs,
    Interview: ApplicationPages.Interview,
    EmployeeAppraisal: ApplicationPages.EmployeeAppraisal,
    Advances: ApplicationPages.Advances,
    Summary: ApplicationPages.Advances
};
var StorePagesMaster = {
    Categories: ApplicationPages.Categories,
    StoreSourceCodes: ApplicationPages.StoreSourceCodes,
    PaymentTypes: ApplicationPages.PaymentTypes,
    Store: ApplicationPages.Store,
    ProcurementSummary: ApplicationPages.ProcurementSummary,
    DepreciationReport: ApplicationPages.DepreciationReport
};


/***/ })

}]);
//# sourceMappingURL=default~accounting-accounting-module~chart-of-accounts-chart-of-accounts-module~configuration-config~44f1780d.js.map
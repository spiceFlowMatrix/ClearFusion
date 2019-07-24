export class GLOBAL {
  public static API_Login_Auth_Url = 'Account/Login';
  public static API_Account_GetUserType = 'Account/UserTypes';
  public static API_AllOffice_URL = 'Code/GetAllOfficeDetails';
  public static API_AllDepartment_Url = 'Account/GetDepartment';
  public static API_Common_GetProviderByMaster = '';
  public static API_UserDetail_AddUser = 'Account/AddUsers';
  public static API_UserDetail_GetUserList = 'Account/GetAllUserDetails';
  public static API_UserRoles_GetRolesList = 'Account/GetRoles';
  public static API_UserRoles_GetUserRolesByUserId = 'Account/GetUserRole';
  public static API_UserRoles_AssignRoleToUser = 'Account/AssignRoleToUser';
  public static API_Permissions_GetPermissionsByRoleId = 'Account/GetPermissionByRoleId';
  public static API_Permissions_GetAllApplicationPages = 'Account/GetAllApplicationPages';
  public static API_Permissions_GetPermissions = 'Account/GetPermissions';
  public static API_Permissions_GetPermissionsOnSelectedRole = 'Account/GetPermissionsOnSelectedRole';
  public static API_Permissions_UpdatePermissionsOnSelectedRole = 'Account/UpdatePermissionsOnSelectedRole';
  public static API_Permissions_AddPermissionInRoles = 'Account/PermissionsInRoles';
  public static API_Permissions_AddRoleWithPagePermissions = 'Account/AddRoleWithPagePermissions';
  public static API_CheckCurrentPassword = 'Account/CheckCurrentPassword';
  public static API_ChangePassword = 'Account/ChangePassword';
  public static API_ResetPassword = 'Account/ResetPassword';
  public static API_UserDetail_GetUserDetailsByUserId = 'Account/GetUserDetailsByUserId';
  public static API_UserDetail_EditUser = 'Account/EditUser';

  public static API_CurrencyCodes_GetAllCurrency = 'Code/GetAllCurrency';
  public static API_EmployeeHR_GetLanguageList = 'EmployeeHR/GetAllLanguages';
  public static API_CurrencyCodes_AddCurrency = 'Code/AddCurrency';
  public static API_CurrencyCode_EditCurrency = 'Code/EditCurrency';
  public static API_OfficeCode_GetAllOfficeDetails = 'Code/GetAllOfficeDetails';
  public static API_OfficeCode_AddOfficeDetail = 'Code/AddOfficeDetail';
  public static API_OfficeCode_EditOfficeDetails = 'Code/EditOfficeDetails';
  public static API_OfficeCode_DeleteOfficeDetails = 'Code/DeleteOfficeDetails';
  public static API_EmailSetting_GetAllEmailSettingDetail = 'Code/GetAllEmailSettingDetail';
  public static API_EmailType_GetAllEmailType = 'Code/GetAllEmailType';
  public static API_EmailSetting_AddEmailSettingDetail = 'Code/AddEmailSettingDetail';
  public static API_EmailSetting_EditEmailSettingDetail = 'Code/EditEmailSettingDetail';
  public static API_JournalCode_GetAllJournalDetail = 'Code/GetAllJournalDetail';
  public static API_JournalCode_AddJournalDetail = 'Code/AddJournalDetail';
  public static API_JournalCode_EditJournalDetail = 'Code/EditJournalDetail';
  public static API_JournalCode_DeleteJournalDetail = 'Code/DeleteJournalDetail';
  public static API_ExchangeRate_GetExchangeRate = 'Code/GetAllExchangeRate';
  public static API_ExchangeRate_AddExchangeRate = 'Code/AddExchangeRate';
  public static API_ExchangeRate_EditExchangeRate = 'Code/EditExchangeRate';
  public static API_ProjectBudget_GetAllProjectBudgetDetail = 'Code/GetAllProjectBudget';
  public static API_ProjectBudget_GetAllProjectDetail = 'Code/GetAllProjectDetails';
  public static API_ProjectBudget_AddProjectDetails = 'Code/AddProjectDetails';
  public static API_ProjectBudget_EditProjectDetails = 'Code/EditProjectDetails';
  public static API_ProjectBudget_AddProjectBudget = 'Code/AddProjectBudget';
  public static API_ProjectBudget_EditProjectBudget = 'Code/EditAllProjectBudget';
  public static API_BudgetLine_GetBudgetLineTypes = 'Code/GetBudgetLineTypes';

  public static API_Accounting_GetAllVoucherDetails = 'Account/GetAllVoucherDetails';
  public static API_Accounting_GetAllVouchersByOfficeId = 'Account/GetAllVouchersByOfficeId';
  public static API_Accounting_GetAllVoucherDetailsByFilter = 'Account/GetAllVoucherDetailsByFilter';
  public static API_Accounting_GetVoucherDetailByVoucherNo = 'Account/GetVoucherDetailByVoucherNo';

  public static API_Accounting_AddVouchers = 'Account/AddVoucherDetail';
  public static API_Accounting_AddExchangeGainLossVoucher = 'Account/AddExchangeGainLossVoucher';
  public static API_Accounting_DeleteExchangeGainLossVoucher = 'Account/DeleteExchangeGainLossVoucher';
  public static API_Accounting_EditVouchers = 'Account/EditVoucherDetail';
  public static API_Accounting_GetVoucherDocumentDetail = 'Account/GetAllVoucherDocumentDetailByVoucherNo';
  public static API_Accounting_AddVouchersDocument = 'Account/AddVoucherDocumentDetail';
  public static API_Accounting_GetAccountDetails = 'Account/GetAllAccountCode';
  public static API_Accounting_GetExchangeGainLossVoucherList = 'Account/GetExchangeGainLossVoucherList';
  public static API_Accounting_GetLevelFourAccountDetails = 'Account/GetLevelFourAccountCode';
  public static API_Accounting_GetAllControlLevelAccountCode = 'Account/GetAllControlLevelAccountCode';
  public static API_Accounting_GetAllInputLevelAccountCode = 'Account/GetAllInputLevelAccountCode';
  public static API_Accounting_GetAllVoucherTransactionDetail = 'Account/GetAllVoucherTransactionDetailByVoucherNo';
  public static API_Accounting_AddVouchersTransaction = 'Account/AddVoucherTransactionDetail';
  public static API_Accounting_EditVouchersTransaction = 'Account/EditVoucherTransactionDetail';
  public static API_Accounting_GetAllJournalDetails = 'Account/GetJouranlVoucherDetails';

  public static API_Accounting_GetAllBudgetLineDetails = 'ProjectPipeLining/GetAllProjectBudgetLineByProjectId';
  public static API_BudgetLine_GetProjectBudgetTransactions = 'Account/GetAllVoucherTransactionDetailByBudgetLine';
  public static API_Accounting_GetProjectAndBudgetLine = 'Account/GetProjectAndBudgetLine';
  public static API_Accounting_GetPrimarySalaryHeads = 'EmployeeHR/GetPrimarySalaryHeads';
  public static API_Accounting_GenerateSalaryVoucher = 'Account/GenerateSalaryVoucher';
  public static API_Accounting_GetEmployeeSalaryVoucher = 'Account/GetEmployeeSalaryVoucher';
  public static API_Accounting_ReverseEmployeeSalaryVoucher = 'Account/ReverseEmployeeSalaryVoucher';


  public static API_ChartOfAccount_GetAllChartAccountDetail = 'Code/GetAllChartAccountDetail';
  public static API_Accounting_GetAllAccoutnType = 'Code/GetAllAccountType';
  public static API_ChartOfAccount_AddChartAccountDetail = 'Code/AddChartAccountDetail';
  public static API_ChartOfAccount_EditChartAccountDetail = 'Code/EditChartAccountDetail';

  // public static API_Hr_GetAllEmployees = 'HREmployee/GetAllEmployeeDetail';
  public static API_Hr_GetAllEmployeeDetail = 'HREmployee/GetAllEmployeeDetail';
  public static API_Accounting_GetEmployeeDocumentDetail = 'HREmployee/GetAllDocumentDetailByEmployeeId';
  public static API_HR_EmployeeDocumentAdd = 'HREmployee/AddDocumentDetail';

  public static API_Hr_AddEmployees = 'HREmployee/AddNewEmployee';
  // public static API_Hr_EditEmployees = 'HREmployee/EditEmployeeDetail';
  public static API_Hr_GetEmployeeById = 'HREmployee/GetEmployeeDetailsByEmployeeId';
  public static API_Hr_AddDocumentDetail = 'HREmployee/AddDocumentDetail';
  public static API_Hr_GetAllDocumentDetails = 'HREmployee/GetAllDocumentDetailByEmployeeId';
  public static API_Hr_EditDocumentDetail = 'HREmployee/EditDocumentDetail';
  public static API_Hr_AddEmployeeProfessionalDetail = 'HREmployee/AddEmployeeProfessionalDetail';
  public static API_Hr_DeleteDocumentDetail = 'HREmployee/DeleteDocumentDetail';
  public static API_Hr_AddEmployeeHistoryDetail = 'HREmployee/AddEmployeeHistoryDetail';
  public static API_Hr_EditEmployeeHistoryDetail = 'HREmployee/EditEmployeeHistoryDetail';
  public static API_Hr_GetAllEmployeeHistoryByEmployeeId = 'HREmployee/GetAllEmployeeHistoryByEmployeeId';
  public static API_Hr_DeleteEmployeeHistoryDetail = 'HREmployee/DeleteEmployeeHistoryDetail';
  public static API_HR_GetAllPayrollMonthlyHourDetail = 'HREmployee/GetAllPayrollMonthlyHourDetail';
  public static API_HR_GetAllPayrollMonthlyHourDetailFilter = 'EmployeeHR/GetAllPayrollMonthlyHourDetail'; // new EmployeeHR
  public static API_EmployeeHR_AddPayrollMonthlyHourDetail = 'EmployeeHR/AddPayrollMonthlyHourDetail';
  // public static API_HR_AddPayrollMonthlyHourDetail = 'HREmployee/AddPayrollMonthlyHourDetail';
  public static API_HR_AssignLeaveToEmployeeDetail = 'HREmployee/AssignLeaveToEmployeeDetail';
  public static API_HR_GetAllEmployeeAssignLeave = 'HREmployee/GetAllEmployeeAssignLeave';
  public static API_HR_GetEmployeeHealthDetail = 'HREmployee/GetAllEmployeeHealthDetailByEmployeeId';
  public static API_HR_AddEmployeeHealthDetail = 'HREmployee/AddEmployeeHealthDetail';
  public static API_HR_EditEmployeeHealthDetail = 'HREmployee/EditEmployeeHealthDetail';
  public static API_HR_GetEmployeeProfessionalDetail = 'HREmployee/GetEmployeeProfessionalDetail';


  public static API_PMU_AddBudgetLine = 'ProjectPipeLining/AddBudgetLine';
  public static API_PMU_EditBudgetLine = 'ProjectPipeLining/EditProjectLineDetail';
  public static API_PMU_GetAllBudgetLineDetails = 'Project/GetProjectBudgetLineDetail';
  public static API_PMU_AddBudgetLineReceivable = 'ProjectPipeLining/AddReceivable';
  public static API_PMU_EditBudgetLineReceivable = 'ProjectPipeLining/EditRecivable';
  public static API_PMU_GetBudgetLineReceivableDetails = 'ProjectPipeLining/GetBudgetLineReceivable';

  public static API_PMU_AddBudgetLineReceived = 'ProjectPipeLining/AddBudgetReceivedAmount';
  public static API_PMU_EditBudgetLineReceived = 'ProjectPipeLining/EditBudgetReceivedAmount';
  public static API_PMU_GetBudgetLineReceivedDetails = 'ProjectPipeLining/GetBudgetReceived';
  public static API_PMU_AddProjectDocument = 'ProjectPipeLining/AddProjectDocument';
  public static API_PMU_GetProjectDocumentDetail = 'ProjectPipeLining/GetProjectDocumentDetail';

  public static API_Code_GetAllProfession = 'Code/GetAllProfession';
  public static API_Code_GetAllQualification = 'Code/GetAllQualification';
  public static API_Code_GetAllCountry = 'Code/GetAllCountry';
  public static API_Code_GetAllProvinceDetails = 'Code/GetAllProvinceDetails';
  public static API_Code_GetAllEmployeeType = 'Code/GetAllEmployeeType';
  public static API_Code_GetAllDesignation = 'Code/GetAllDesignation';
  public static API_Code_GetDepartmentsByOfficeId = 'Code/GetDepartmentsByOfficeId';
  public static API_Code_LeaveReasonType = 'Code/GetAllLeaveReasonList';
  public static API_Code_AddLeaveReasonDetail = 'Code/AddLeaveReasonDetail';
  public static API_Code_EditLeaveReasonDetail = 'Code/EditLeaveReasonDetail';
  public static API_Code_GetAllFinancialYearDetail = 'Code/GetAllFinancialYearDetail';
  public static API_Code_AddEditPensionDebitAccount = 'Code/AddEditPensionDebitAccount';
  public static API_Code_GetPensionDebitAccount = 'Code/GetPensionDebitAccount';
  public static API_Code_GetAttendanceGroups = 'Code/GetAttendanceGroups';
  public static API_Code_AddAttendanceGroups = 'Code/AddAttendanceGroups';
  public static API_Code_EditAttendanceGroups = 'Code/EditAttendanceGroups';

  public static API_PMU_GetBudgetLinePayableDetails = 'ProjectPipeLining/GetBudgetLinePayable';
  public static API_PMU_AddBudgetLinePayable = 'ProjectPipeLining/AddBudgetLinePayable';
  public static API_PMU_EditBudgetLinePayable = 'ProjectPipeLining/EditBudgetLinePayable';
  public static API_PMU_GetProjectBudgetSummary = 'ProjectPipeLining/GetBudgetLineSummary';
  public static API_PMU_GetBudgetLinePaidDetails = 'ProjectPipeLining/GetBudgetLinePaid';
  public static API_PMU_AddBudgetLinePaid = 'ProjectPipeLining/AddBudgetLinePaidAmount';
  public static API_PMU_EditBudgetLinePaid = 'ProjectPipeLining/EditBudgetLinePaidAmount';

  public static API_TaskAndActivity_GetAllTask = 'TaskAndActivity/GetAllTask';
  public static API_TaskAndActivity_AddTask = 'TaskAndActivity/AddTask';
  public static API_TaskAndActivity_EditTask = 'TaskAndActivity/EditTask';
  public static API_TaskAndActivity_GetAllActivity = 'TaskAndActivity/GetAllActivity';
  public static API_TaskAndActivity_AddActivityDetail = 'TaskAndActivity/AddActivityDetail';
  public static API_TaskAndActivity_EditActivityDetail = 'TaskAndActivity/EditActivityDetail';

  public static API_HR_ChangeEmployeeImage = 'HREmployee/ChangeEmployeeImage';
  // public static API_HR_GetAllActiveEmployeeForAttendance = 'HREmployee/GetAllActiveEmployeeForAttendance';
  // public static API_HR_AddEmployeeAttendanceDetails = 'HREmployee/AddEmployeeAttendanceDetails';
  public static API_HR_AddEmployeeAttendanceDetails = 'EmployeeHR/AddEmployeeAttendanceDetails'; // new
  public static API_HR_GetEmployeeAttendanceDetails = 'HREmployee/GetEmployeeAttendanceDetails';

  public static API_Code_AddFinancialYearDetail = 'Code/AddFinancialYearDetail';
  public static API_Code_EditFinancialYearDetail = 'Code/EditFinancialYearDetail';

  public static API_HR_GetAllJobHiringDetails = 'HREmployee/GetAllJobHiringDetails';
  public static API_HR_AddJobHiringDetail = 'HREmployee/AddJobHiringDetail';
  public static API_HR_EditJobHiringDetail = 'HREmployee/EditJobHiringDetail';
  public static API_EmployeeHR_GetJobCode = 'EmployeeHR/GetJobCode';

  // public static API_HR_EditPayrollMonthlyHourDetail = 'HREmployee/EditPayrollMonthlyHourDetail';
  public static API_HR_EditPayrollMonthlyHourDetail = 'EmployeeHR/EditPayrollMonthlyHourDetail'; // new

  public static API_Account_GetAllNotesDetails = 'Account/GetAllNotesDetails';
  // public static API_HR_GetAllProspectiveEmployee = 'HREmployee/GetAllProspectiveEmployee';
  public static API_HR_GetProspectiveEmployeesByProfessionId = 'HREmployee/GetProspectiveEmployeesByProfessionId';



  public static API_Code_GetAllInterviewRoundList = 'Code/GetAllInterviewRoundList';
  public static API_HR_AddInterviewScheduleDetails = 'HREmployee/AddInterviewScheduleDetails';
  public static API_HR_GetAllScheduledProspectiveEmployee = 'HREmployee/GetAllScheduledProspectiveEmployee';

  public static API_HR_GetAllJobGrade = 'HREmployee/GetAllJobGrade';
  // tslint:disable-next-line:max-line-length
  public static API_HR_GetAllScheduledProspectiveEmployeeForGeneralAssembly = 'HREmployee/GetAllScheduledProspectiveEmployeeForGeneralAssembly';
  public static API_HR_GetAllScheduledProspectiveEmployeeForDirector = 'HREmployee/GetAllScheduledProspectiveEmployeeForDirector';
  public static API_HR_GetAllScheduledProspectiveEmployeeForGeneralAdmin = 'HREmployee/GetAllScheduledProspectiveEmployeeForGeneralAdmin';
  public static API_HR_GetAllScheduledProspectiveEmployeeForFieldOffice = 'HREmployee/GetAllScheduledProspectiveEmployeeForFieldOffice';

  public static API_HR_GetAllScheduledEmployeeList = 'HREmployee/GetAllScheduledEmployeeList';
  public static API_HR_InterviewApprovals = 'HREmployee/InterviewApprovals';
  public static API_HR_GetAllApprovedEmployeeList = 'HREmployee/GetAllApprovedEmployeeList';

  public static API_Account_AddNotesDetails = 'Account/AddNotesDetails';
  public static API_Account_EditNotesDetails = 'Account/EditNotesDetails';
  public static API_Account_GetBalanceSheetDetails = 'Account/GetBlanceSheetDetails';
  public static API_Account_GetFinancialYearDetails = 'Code/GetCurrentFinancialYear';
  public static API_Account_GetDetailsOfNotes = 'Account/GetDetailsOfNotes';
  public static API_Account_GetDetailsOfNotesReportData = 'Account/GetDetailsOfNotesReportData';



  public static API_Profession_AddProfession = 'Code/AddProfession';
  public static API_Profession_EditProfession = 'Code/EditProfession';
  public static API_Department_GetAllDepartment = 'Code/GetAllDepartment';
  public static API_Department_AddDepartment = 'Code/AddDepartment';
  public static API_Department_EditDepartment = 'Code/EditDepartment';

  public static API_HR_AddEmployeeSalaryDetail = 'HREmployee/AddEmployeeSalaryDetail';
  public static API_HR_EditEmployeeSalaryDetail = 'HREmployee/EditEmployeeSalaryDetail';
  public static API_EmployeeHR_EditAccountEmployeeSalaryDetail = 'EmployeeHR/EditEmployeeAccountSalaryDetail';
  public static API_HR_GetEmployeePayrollDetails = 'HREmployee/GetEmployeeSalaryDetailsByEmployeeId';

  public static API_HR_AddEmployeeApplyLeaveDetail = 'HREmployee/AddEmployeeApplyLeaveDetail';
  public static API_HR_GetEmployeeApplyLeaveDetailById = 'HREmployee/GetEmployeeApplyLeaveDetailById';
  public static API_HR_GetAllEmployeeApplyLeaveList = 'HREmployee/GetAllEmployeeApplyLeaveList';
  public static API_HR_ApproveEmployeeLeave = 'HREmployee/ApproveEmployeeLeave';
  public static API_HR_RejectEmployeeLeave = 'HREmployee/RejectEmployeeLeave';
  public static API_HR_DeleteApplyEmployeeLeave = 'HREmployee/DeleteApplyEmployeeLeave';

  // public static API_HR_GetAllEmployeeMonthlyPayrollList = 'HREmployee/GetAllEmployeeMonthlyPayrollList';
  public static API_EmployeeHR_GetAllEmployeeMonthlyPayrollList = 'EmployeeHR/GetAllEmployeeMonthlyPayrollList';
  public static API_HR_GetAllEmployeeMonthlyPayrollListApproved = 'EmployeeHR/GetAllEmployeeMonthlyPayrollListApproved';

  public static API_HR_EditEmployeeProfessionalDetail = 'HREmployee/EditEmployeeProfessionalDetail';

  public static API_Code_GetCurrentFinancialYear = 'Code/GetCurrentFinancialYear';
  public static API_Hr_EditEmployeeDetail = 'HREmployee/EditEmployeeDetail';
  public static API_Code_EditDesignation = 'Code/EditDesignation';
  public static API_Code_AddDesignation = 'Code/AddDesignation';
  public static API_Code_AddQualificationDetails = 'Code/AddQualificationDetails';
  public static API_Code_EditQualifactionDetails = 'Code/EditQualifactionDetails';
  public static API_Code_DeleteQualifactionDetails = 'Code/DeleteQualifactionDetails';



  public static API_Hr_GetAllEmployeesAttendanceByDate = 'HREmployee/GetAllEmployeesAttendanceByDate';
  public static API_Hr_EditEmployeeAttendanceByDate = 'HREmployee/EditEmployeeAttendanceByDate';


  public static API_Hr_GetAllHolidayDetails = 'HREmployee/GetAllHolidayDetails';
  public static API_Hr_AddHolidayDetails = 'HREmployee/AddHolidayDetails';
  public static API_Hr_EditHolidayDetails = 'HREmployee/EditHolidayDetails';


  public static API_Hr_AddJobGradeDetail = 'HREmployee/AddJobGradeDetail';


  public static API_HR_AddJobGradeDetail = 'HREmployee/AddJobGradeDetail';
  public static API_HR_EditJobGradeDetail = 'HREmployee/EditJobGradeDetail';
  public static API_HR_EditEmployeeAssignLeave = 'HREmployee/EditEmployeeAssignLeave';
  public static API_HR_DeleteHolidayDetails = 'HREmployee/DeleteHolidayDetails';


  public static API_HR_GetAllDisableCalanderDate = 'HREmployee/GetAllDisableCalanderDate';


  public static API_HR_MonthlyEmployeeAttendanceReport = 'HREmployee/MonthlyEmployeeAttendanceReport';


  public static API_HR_GetAllDateforDisableCalenderDate = 'HREmployee/GetAllDateforDisableCalenderDate';


  public static API_HR_GetAllHolidayWeeklyDetails = 'HREmployee/GetAllHolidayWeeklyDetails';


  public static API_Code_GetAllSalaryHead = 'Code/GetAllSalaryHead';
  public static API_Code_GetAllPayrollHead = 'Code/GetAllPayrollHead';
  public static API_Code_EditSalaryHead = 'Code/EditSalaryHead';
  public static API_Code_AddSalaryHead = 'Code/AddSalaryHead';
  public static API_Code_AddPayrollAccountHead = 'Code/AddPayrollAccountHead';
  public static API_Code_UpdatePayrollAccountHead = 'Code/UpdatePayrollAccountHead';
  public static API_Code_DeletePayrollAccountHead = 'Code/DeletePayrollAccountHead';
  public static API_Code_UpdatePayrollAccountHeadAllEmployees = 'Code/UpdatePayrollAccountHeadAllEmployees';
  public static API_Code_DeleteSalaryHead = 'Code/DeleteSalaryHead';




  public static API_HR_EmployeesSalarySummary = 'HREmployee/EmployeesSalarySummary';
  // public static API_HR_EmployeesPayrollRegisterApproval = 'HREmployee/EmployeePaymentTypeReport';
  public static API_HR_EmployeesPayrollRegisterApproval = 'EmployeeHR/EmployeePaymentTypeReport';
  public static API_Account_DisapproveEmployeeApprovedSalary = 'Account/DisapproveEmployeeApprovedSalary';

  public static API_HR_RemoveApprovedList = 'HREmployee/RemoveApprovedList';

  // public static API_HR_EmployeePensionReport = 'HREmployee/EmployeePensionReport';
  public static API_HR_EmployeePensionReport = 'EmployeeHR/EmployeePensionReport';

  public static API_PMU_GetAllEmployeesBudgetLine = 'ProjectPipeLining/GetAssignedEmployeesInBudgetLine';
  public static API_PMU_AssignEmployeeToBudgetLine = 'ProjectPipeLining/AssignEmployeeToBudgetLine';
  public static API_Hr_GetAllEmployeeProject = 'HREmployee/GetAllEmployeeProjects';
  public static API_Hr_AssignEmployeeProjectPercentage = 'HREmployee/AssignEmployeeProjectPercentage';
  public static API_Code_AddPensionRate = 'Code/AddPensionRate';
  public static API_Code_EditPensionRate = 'Code/EditPensionRate';
  public static API_Code_GetAllPensionRate = 'Code/GetAllPensionRate';
  public static API_Hr_GetExchangeRate = 'HREmployee/GetExchangeRate';

  public static API_Hr_GetEmployeeContractType = 'HREmployee/GetAllEmployeeContractType';
  public static API_Code_SaveContractContent = 'HREmployee/SaveContractContent';
  public static API_Code_GetAllContractTypeContent = 'HREmployee/GetAllContractTypeContent';
  // public static API_Code_GetSelectedEmployeeContract = 'HREmployee/GetSelectedEmployeeContract';

  public static API_Hr_GetEmployeeSalaryDetails = 'HREmployee/GetEmployeeSalaryDetails';
  public static API_Hr_EmployeeTaxCalculation = 'HREmployee/EmployeeTaxCalculation';


  public static API_Code_GetAppraisalQuestions = 'Code/GetAppraisalQuestions';
  public static API_Code_AddAppraisalQuestion = 'Code/AddAppraisalQuestion';
  public static API_Code_EditAppraisalQuestion = 'Code/EditAppraisalQuestion';

  public static API_Code_GetAllEmployeeAppraisalDetails = 'Code/GetAllEmployeeAppraisalDetails';
  public static API_Code_GetAllEmployeeAppraisalDetailsByEmployeeId = 'Code/GetAllEmployeeAppraisalDetailsByEmployeeId';
  public static API_Code_AddEmployeeAppraisalDetails = 'Code/AddEmployeeAppraisalDetails';
  public static API_Code_EditEmployeeAppraisalDetails = 'Code/EditEmployeeAppraisalDetails';

  public static API_Code_GetEmployeeDetailByOfficeId = 'Code/GetEmployeeDetailByOfficeId';
  public static API_Code_GetEmployeeAdvanceHistoryDetail = 'Code/GetEmployeeAdvanceHistoryDetail';
  public static API_EmployeeHR_GetEmployeePensionHistoryDetail = 'EmployeeHR/GetEmployeePensionHistoryDetail';
  public static API_Code_GetEmployeeDetailByEmployeeId = 'Code/GetEmployeeDetailByEmployeeId';

  public static API_Code_GetInterviewQuestions = 'Code/GetAllInterviewTechnicalQuestionsByOfficeId';
  public static API_Code_AddInterviewQuestion = 'Code/AddInterviewTechnicalQuestions';
  public static API_Code_EditInterviewQuestion = 'Code/EditInterviewTechnicalQuestions';
  public static API_Code_GetAllEmployeeAppraisalMoreDetails = 'Code/GetAllEmployeeAppraisalMoreDetails';
  public static API_Code_AddEmployeeAppraisalMoreDetails = 'Code/AddEmployeeAppraisalMoreDetails';
  public static API_Code_EditEmployeeAppraisalMoreDetails = 'Code/EditEmployeeAppraisalMoreDetails';

  public static API_Hr_GetAllAdvancesByOfficeId = 'HREmployee/GetAllAdvancesByOfficeId';
  public static API_EmployeeHR_GetAllEmployeePension = 'EmployeeHR/GetAllEmployeePension';
  public static API_Account_AddEmployeePensionPayment = 'Account/AddEmployeePensionPayment';
  public static API_Hr_AddAdvances = 'HREmployee/AddAdvances';
  public static API_Hr_EditAdvances = 'HREmployee/EditAdvances';
  public static API_Hr_ApproveAdvances = 'HREmployee/ApproveAdvances';

  public static API_Code_GetAllExitInterview = 'Code/GetAllExitInterview';
  public static API_Code_AddExitInterview = 'Code/AddExitInterview';
  public static API_Code_EditExitInterview = 'Code/EditExitInterview';
  public static API_Code_DeleteExitInterview = 'Code/DeleteExitInterview';

  public static API_Hr_GetAllInterviewDetails = 'HREmployee/GetAllInterviewDetails';
  public static API_Hr_AddInterviewDetails = 'HREmployee/AddInterviewDetails';
  public static API_Hr_EditInterviewDetails = 'HREmployee/EditInterviewDetails';

  public static API_Code_ApproveEmployeeAppraisalRequest = 'Code/ApproveEmployeeAppraisalRequest';
  public static API_Code_RejectEmployeeAppraisalRequest = 'Code/RejectEmployeeAppraisalRequest';



  public static API_Code_ApproveEmployeeInterviewRequest = 'Code/ApproveEmployeeInterviewRequest';
  public static API_Code_RejectEmployeeInterviewRequest = 'Code/RejectEmployeeInterviewRequest';

  public static API_Code_ApproveEmployeeEvaluationRequest = 'Code/ApproveEmployeeEvaluationRequest';
  public static API_Code_RejectEmployeeEvaluationRequest = 'Code/RejectEmployeeEvaluationRequest';

  public static API_Accounting_DeleteVoucherTransactionDetail = 'Account/DeleteVoucherTransactionDetail';
  public static API_Accounting_DeleteVoucherTransactions = 'Account/DeleteVoucherTransactions';

  public static API_Accounting_AddCategoryPopulator = 'Account/AddCategoryPopulator';
  public static API_Accounting_EditCategoryPopulator = 'Account/EditCategoryPopulator';
  public static API_Accounting_DeleteCategoryPopulator = 'Account/DeleteCategoryPopulator';
  public static API_Accounting_GetAllCategoryPopulator = 'Account/GetAllCategoryPopulator';


  public static API_Accounting_GetAllNotifications = 'Account/GetAllUserNotifications';


  public static API_Store_GetAllInventories = 'Store/GetAllInventories';
  public static API_Store_AddInventory = 'Store/AddInventory';
  public static API_Store_EditInventory = 'Store/EditInventory';
  public static API_Store_DeleteInventory = 'Store/DeleteInventory';
  public static API_Store_GetInventoryCode = 'Store/GetInventoryCode';
  public static API_Store_GetInventoryItemCode = 'Store/GetInventoryItemCode';

  public static API_Store_GetAllStoreItemGroups = 'Store/GetAllStoreItemGroups';
  public static API_Store_AddStoreItemGroup = 'Store/AddStoreItemGroup';
  public static API_Store_EditStoreItemGroup = 'Store/EditStoreItemGroup';
  // public static API_Store_DeleteInventoryItems = 'Store/DeleteInventoryItems';
  public static API_Store_GetStoreGroupItemCode = 'Store/GetStoreGroupItemCode';

  public static API_Store_GetAllInventoryItems = 'Store/GetAllInventoryItems';
  public static API_Store_AddInventoryItems = 'Store/AddInventoryItems';
  public static API_Store_EditInventoryItems = 'Store/EditInventoryItems';
  public static API_Store_DeleteInventoryItems = 'Store/DeleteInventoryItems';

  public static API_Store_GetAllInventoryItemsType = 'Store/GetAllInventoryItemsType';
  public static API_Store_AddInventoryItemsType = 'Store/AddInventoryItemsType';
  public static API_Store_EditInventoryItemsType = 'Store/EditInventoryItemsType';
  public static API_Store_DeleteInventoryItemsType = 'Store/DeleteInventoryItemsType';

  public static API_Store_GetAllPurchasesByItem = 'Store/GetAllPurchasesByItem';
  public static API_Store_AddPurchase = 'Store/AddPurchase';
  public static API_Store_EditPurchase = 'Store/EditPurchase';
  public static API_Store_DeletePurchase = 'Store/DeletePurchase';
  public static API_Store_VerifyPurchase = 'Store/VerifyPurchase';
  public static API_Store_UnverifyPurchase = 'Store/UnverifyPurchase';

  public static API_Store_GetAllPurchaseUnitType = 'Store/GetAllPurchaseUnitType';
  public static API_Store_AddPurchaseUnitType = 'Store/AddPurchaseUnitType';
  public static API_Store_EditPurchaseUnitType = 'Store/EditPurchaseUnitType';
  public static API_Store_DeletePurchaseUnitType = 'Store/DeletePurchaseUnitType';

  public static API_Store_GetAllItemsOrder = 'Store/GetAllItemsOrder';
  public static API_Store_AddItemOrder = 'Store/AddItemOrder';
  public static API_Store_EditItemOrder = 'Store/EditItemOrder';
  public static API_Store_DeleteItemOrder = 'Store/DeleteItemOrder';


  public static API_Store_GetItemAmounts = 'Store/GetItemAmounts';
  public static API_Store_GetProcurementSummary = 'Store/GetProcurementSummary';
  public static API_Store_GetAllDepreciationByFilter = 'Store/GetAllDepreciationByFilter';
  public static API_Store_UpdateInvoice = 'Store/UpdateInvoice';
  public static API_Store_UpdatePurchaseImage = 'Store/UpdatePurchaseImage';
  public static API_Store_GetAllPurchaseInvoices = 'Store/GetAllPurchaseInvoices';
  public static API_Store_GetAllStoreSourceCode = 'Store/GetAllStoreSourceCode';
  public static API_Store_GetAllStoreSourceType = 'Store/GetAllStoreSourceType';
  public static API_Store_AddStoreSourceCode = 'Store/AddStoreSourceCode';
  public static API_Store_GetStoreTypeCode= 'Store/GetStoreTypeCode';
  public static API_Store_EditStoreSourceCode = 'Store/EditStoreSourceCode';
  public static API_Store_DeleteStoreSourceCode = 'Store/DeleteStoreSourceCode';
  public static API_Store_GetAllPaymentTypes = 'Store/GetAllPaymentTypes';
  public static API_Store_AddPaymentTypes = 'Store/AddPaymentTypes';
  public static API_Store_EditPaymentTypes = 'Store/EditPaymentTypes';
  public static API_Store_DeletePaymentTypes = 'Store/DeletePaymentTypes';

  // public static API_Hr_GetAllEmployeeMonthlyPayrollListApproved = 'HREmployee/GetAllEmployeeMonthlyPayrollListApproved';
  public static API_EmployeeHr_GetAllEmployeeMonthlyPayrollListApproved = 'EmployeeHR/GetAllEmployeeMonthlyPayrollListApproved';

  // public static API_Hr_EmployeePaymentTypeReportForSaveOnly = 'HREmployee/EmployeePaymentTypeReportForSaveOnly'; //old
    public static API_EmployeeHr_EmployeePaymentTypeReportForSaveOnly = 'EmployeeHR/EmployeePaymentTypeReportForSaveOnly'; // new

  // public static API_Hr_EmployeeSalaryTaxDetails = 'HREmployee/EmployeeSalaryTaxDetails';
  public static API_Hr_EmployeeSalaryTaxDetails = 'EmployeeHR/EmployeeSalaryTaxDetails';
  public static API_Hr_AddEmployeeContractDetails = 'HREmployee/AddEmployeeContractDetails';
  public static API_Hr_RemoveEmployeeContractDetails = 'HREmployee/RemoveEmployeeContractDetails';
  public static API_Hr_GetSelectedEmployeeContractByEmployeeId = 'HREmployee/GetSelectedEmployeeContractByEmployeeId';

  public static API_Notification_SetNotificationIsReadFlag = 'Notification/SetNotificationIsReadFlag';
  public static API_Notification_GetNotificationIsReadCount = 'Notification/GetNotificationIsReadCount';


  public static API_Accounting_GetExchangeGainOrLossAmount = 'Account/GetExchangeGainOrLossAmount';
  public static API_Accounting_GetExchangeGainOrLossTransactionAmount = 'Account/GetExchangeGainOrLossTransactionAmount';


  public static API_Code_GetSalaryTaxReportContentDetails = 'Code/GetSalaryTaxReportContentDetails';
  public static API_Code_AddSalaryTaxReportContentDetails = 'Code/AddSalaryTaxReportContentDetails';
  public static API_Code_EditSalaryTaxReportContentDetails = 'Code/EditSalaryTaxReportContentDetails';


  public static API_Store_AddItemSpecificationsMaster = 'Store/AddItemSpecificationsMaster';
  public static API_Store_EditItemSpecificationsMaster = 'Store/EditItemSpecificationsMaster';
  public static API_Store_GetItemSpecificationsMaster = 'Store/GetItemSpecificationsMaster';



  public static API_Store_AddItemSpecificationsDetails = 'Store/AddItemSpecificationsDetails';
  public static API_Store_EditItemSpecificationsDetails = 'Store/EditItemSpecificationsDetails';
  public static API_Store_GetAllItemSpecificationsDetails = 'Store/GetAllItemSpecificationsDetails';

  public static API_Store_GetAllStatusAtTimeOfIssue = 'Store/GetAllStatusAtTimeOfIssue';
  public static API_Store_GetAllReceiptType = 'Store/GetAllReceiptType';


  public static API_EmployeeDetail_GetAllEmployeeEducations = 'EmployeeDetail/GetAllEmployeeEducations';
  public static API_EmployeeDetail_AddEmployeeEducations = 'EmployeeDetail/AddEmployeeEducations';
  public static API_EmployeeDetail_EditEmployeeEducations = 'EmployeeDetail/EditEmployeeEducations';
  public static API_EmployeeDetail_DeleteEmployeeEducations = 'EmployeeDetail/DeleteEmployeeEducations';

  public static API_EmployeeDetail_GetAllEmployeeSalaryBudgets = 'EmployeeDetail/GetAllEmployeeSalaryBudgets';
  public static API_EmployeeDetail_AddEmployeeSalaryBudgets = 'EmployeeDetail/AddEmployeeSalaryBudgets';
  public static API_EmployeeDetail_EditEmployeeSalaryBudgets = 'EmployeeDetail/EditEmployeeSalaryBudgets';
  public static API_EmployeeDetail_DeleteEmployeeSalaryBudgets = 'EmployeeDetail/DeleteEmployeeSalaryBudgets';

  public static API_EmployeeDetail_GetAllEmployeeOtherSkills = 'EmployeeDetail/GetAllEmployeeOtherSkills';
  public static API_EmployeeDetail_AddEmployeeOtherSkills = 'EmployeeDetail/AddEmployeeOtherSkills';
  public static API_EmployeeDetail_EditEmployeeOtherSkills = 'EmployeeDetail/EditEmployeeOtherSkills';
  public static API_EmployeeDetail_DeleteEmployeeOtherSkills = 'EmployeeDetail/DeleteEmployeeOtherSkills';

  public static API_EmployeeDetail_GetAllEmployeeInfoReferences = 'EmployeeDetail/GetAllEmployeeInfoReferences';
  public static API_EmployeeDetail_AddEmployeeInfoReferences = 'EmployeeDetail/AddEmployeeInfoReferences';
  public static API_EmployeeDetail_EditEmployeeInfoReferences = 'EmployeeDetail/EditEmployeeInfoReferences';
  public static API_EmployeeDetail_DeleteEmployeeInfoReferences = 'EmployeeDetail/DeleteEmployeeInfoReferences';

  public static API_EmployeeDetail_GetAllEmployeeRelativeInformation = 'EmployeeDetail/GetAllEmployeeRelativeInformation';
  public static API_EmployeeDetail_AddEmployeeRelativeInformation = 'EmployeeDetail/AddEmployeeRelativeInformation';
  public static API_EmployeeDetail_EditEmployeeRelativeInformation = 'EmployeeDetail/EditEmployeeRelativeInformation';
  public static API_EmployeeDetail_DeleteEmployeeRelativeInformation = 'EmployeeDetail/DeleteEmployeeRelativeInformation';

  public static API_EmployeeDetail_GetAllEmployeeHistoryOutsideCountry = 'EmployeeDetail/GetAllEmployeeHistoryOutsideCountry';
  public static API_EmployeeDetail_AddEmployeeHistoryOutsideCountry = 'EmployeeDetail/AddEmployeeHistoryOutsideCountry';
  public static API_EmployeeDetail_EditEmployeeHistoryOutsideCountry = 'EmployeeDetail/EditEmployeeHistoryOutsideCountry';
  public static API_EmployeeDetail_DeleteEmployeeHistoryOutsideCountry = 'EmployeeDetail/DeleteEmployeeHistoryOutsideCountry';

  public static API_EmployeeDetail_GetAllEmployeeHistoryOutsideOrganization = 'EmployeeDetail/GetAllEmployeeHistoryOutsideOrganization';
  public static API_EmployeeDetail_AddEmployeeHistoryOutsideOrganization = 'EmployeeDetail/AddEmployeeHistoryOutsideOrganization';
  public static API_EmployeeDetail_EditEmployeeHistoryOutsideOrganization = 'EmployeeDetail/EditEmployeeHistoryOutsideOrganization';
  public static API_EmployeeDetail_DeleteEmployeeHistoryOutsideOrganization = 'EmployeeDetail/DeleteEmployeeHistoryOutsideOrganization';

  public static API_EmployeeDetail_GetAllEmployeeLanguages = 'EmployeeDetail/GetAllEmployeeLanguages';
  public static API_EmployeeDetail_AddEmployeeLanguages = 'EmployeeDetail/AddEmployeeLanguages';
  public static API_EmployeeDetail_EditEmployeeLanguages = 'EmployeeDetail/EditEmployeeLanguages';
  public static API_EmployeeDetail_RemoveEmployeeLanguages = 'EmployeeDetail/RemoveEmployeeLanguages';

  public static API_EmployeeDetail_GetAllEmployeeSalaryAnalyticalInfo = 'EmployeeDetail/GetAllEmployeeSalaryAnalyticalInfo';
  public static API_EmployeeDetail_AddEmployeeSalaryAnalyticalInfo = 'EmployeeDetail/AddEmployeeSalaryAnalyticalInfo';
  public static API_EmployeeDetail_EditEmployeeSalaryAnalyticalInfo = 'EmployeeDetail/EditEmployeeSalaryAnalyticalInfo';
  public static API_EmployeeDetail_DeleteEmployeeSalaryAnalyticalInfo = 'EmployeeDetail/DeleteEmployeeSalaryAnalyticalInfo';


  public static API_EmployeeDetail_GetAllEmployeeHealthInfo = 'EmployeeDetail/GetAllEmployeeHealthInfo';
  public static API_EmployeeDetail_AddEmployeeHealthInfo = 'EmployeeDetail/AddEmployeeHealthInfo';
  public static API_EmployeeDetail_EditEmployeeHealthInfo = 'EmployeeDetail/EditEmployeeHealthInfo';


  public static API_EmployeeDetail_GetEmployeeHealthQuestion = 'EmployeeDetail/GetEmployeeHealthQuestion';
  public static API_EmployeeDetail_AddEmployeeHealthQuestion = 'EmployeeDetail/AddEmployeeHealthQuestion';
  public static API_EmployeeDetail_EditEmployeeHealthQuestion = 'EmployeeDetail/EditEmployeeHealthQuestion';
  public static API_EmployeeDetail_DeleteEmployeeHealthQuestion = 'EmployeeDetail/DeleteEmployeeHealthQuestion';

  public static API_Accounting_GetAllVoucherByJouranlId = 'Account/GetAllVoucherByJouranlId';
  public static API_EmployeeHR_AddEmployeeLeaveDetails = 'EmployeeHR/AddEmployeeLeaveDetails';
  public static API_Code_GetApplicationPages = 'Code/GetApplicationPages';

  // Reports
  public static API_AccountReports_GetAllLedgerDetails = 'AccountReports/GetAllLedgerDetails';
  public static API_AccountReports_GetTrialBalanceReport = 'AccountReports/GetTrialBalanceReport';
  public static API_AccountReports_GetJournalVoucherDetailsByCondition= 'AccountReports/GetJournalVoucherDetailsByCondition';








  // public static API_Store_GetAllItemSpecificationDetails = 'Store/GetAllItemSpecificationDetails';
  // public static API_Store_AddItemSpecification = 'Store/AddItemSpecification';
  // public static API_Store_EditItemSpecification = 'Store/EditItemSpecification';
  // public static API_Store_DeleteItemSpecification = 'Store/DeleteItemSpecification';
}

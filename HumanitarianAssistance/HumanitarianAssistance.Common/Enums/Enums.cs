using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Common.Enums
{

  public enum BalanceType
  {
    SUM = 1,
    DR = 2,
    CR = 3
  }
  public enum AccountType
  {

    CapitalAssetsWrittenOff = 1,
    CurrentAssets = 2,
    Funds = 3,
    EndownmentFund = 4,
    ReserveAccountAdjustment = 5,
    LongtermLiability = 6,
    CurrentLiability = 7,
    ReserveAccount = 8,
    IncomeFromDonor = 9,
    IncomeFromProjects = 10,
    ProfitOnBankDeposits = 11,
    IncomeExpenditureFund = 12
  }

  public enum FinancialReportType
  {
    BALANCESHEET = 1,
    INCOMEANDEXPANCE = 2
  }

  public enum Gender
  {
    MALE = 1,
    FEMALE = 2,
    OTHER = 3
  }

  public enum AttendanceType
  {
    P = 1,
    A = 2,
    L = 3,
    H = 4
  }

  public enum ApplyLeaveStatus
  {
    Approve = 1,
    Reject = 2
  }

  public enum EmployeeTypeStatus
  {
    Prospective = 1,
    Active = 2,
    Terminated = 3
  }

  public enum HolidayType
  {
    PARTICULARDATE = 1,
    REPEATWEEKLYDAY = 2
  }

  public enum SalaryHeadType
  {
    ALLOWANCE = 1,
    DEDUCTION = 2,
    GENERAL = 3
  }

  public enum RECORDTYPE
  {
    SINGLE = 1,
    CONSOLIDATE = 2
  }

  public enum Sex
  {
    Male = 1,
    Female = 2,
    Other = 3
  }

  public enum LoggerEnum
  {
    VoucherCreated = 1,
    VoucherUpdate = 2,
    VoucherDeleted = 3,
    EmployeeCreated = 4,
    EmployeeUpdate = 5,
    EmployeeDeleted = 6
  }
  public enum AssetType
  {
    Cash = 1,
    InKind = 2
  }

  public enum Currency
  {
    AFG = 1,
    EUR = 2,
    PKR = 3,
    USD = 4
  }

  public enum AccountLevels
  {
    MainLevel = 1,
    ControlLevel = 2,
    SubLevel = 3,
    InputLevel = 4
  }
  public enum TransactionType
  {
    Credit=1,
    Debit=2
  }

  public enum VoucherTypes
  {
    Journal = 1,
    Adjustment = 2
  }

}

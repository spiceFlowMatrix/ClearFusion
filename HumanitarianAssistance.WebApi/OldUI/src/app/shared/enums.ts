export enum RequestStatus {
  InternalError = 500,
  NotFound = 400,
  NotAcceptable = 406,
  Confilct = 409,
  Forbidden = 403
}

export enum UserType {
  Admin = 1,
  Master = 2,
  Other = 3,
  Provider = 15
}

export enum LanguageID {
  English = 1,
  French = 2,
  Spanish = 3
}

export enum Notes {
  Note1 = 1,
  Note2 = 2,
  Note3 = 3,
  Note4 = 4,
  Note5 = 5,
  Note6 = 6,
  Note7 = 7,
  Note8 = 8,
  Note9 = 9,
  Note10 = 10,
  Note11 = 11,
  Note12 = 12,
  Note13 = 13,
  Note14 = 14,
  Note15 = 15,
  Note16 = 16,
  Note17 = 17,
  Note18 = 18,
  Note19 = 19,
  Note20 = 20,
  Note21 = 21,
  Note22 = 22,
  Note23 = 23,
  Note24 = 24,
  Note25 = 25,
  Note26 = 26,
  Note27 = 27,
  Note28 = 28,
  Note29 = 29,
  Note30 = 30,
  Note31 = 31,
  Note32 = 32,
  Note33 = 33,
  Note34 = 34,
  Note35 = 35,
  Note36 = 36,
  Note37 = 37,
  Note38 = 38,
  Note39 = 39,
  Note40 = 40,
  Note41 = 41,
  Note42 = 42,
  Note43 = 43,
  Note44 = 44,
  Note45 = 45,
  Note46 = 46,
  Note47 = 47,
  Note48 = 48,
  Note49 = 49,
  Note50 = 50
}

export enum AccountGroup {
  CurrentAssets = 1,
  LongTermAssets = 2,
  CurrentLiabilities = 3,
  LongTermLiabilities = 4,
  Equity = 5,
  Income = 6,
  CostOfSales = 7,
  Expenses = 8
}

export enum BalanceType {
  Sum = 1,
  DR = 2,
  CR = 3
}

export enum FinancialStatement {
  BalanceSheet = 1,
  IncomeAndExpenses = 2
}

export enum LoggerEnum {
  VoucherCreated = 1,
  VoucherUpdate = 2,
  VoucherDeleted = 3,
  EmployeeCreated = 4,
  EmployeeUpdate = 5,
  EmployeeDeleted = 6
}

export enum PayrollHeadName {
  NetSalary = 1,
  AdvanceDeduction = 2,
  SalaryTax = 3,
  GrossSalary = 4,
  Pension = 5
}

export enum SalaryHeadType {
  ALLOWANCE = 1,
  DEDUCTION = 2,
  GENERAL = 3
}

export enum TransactionType {
  Credit = 1,
  Debit = 2
}

export enum StoreSelectedInventory {
  C = 1,
  E = 2,
  N = 3
}

export enum SourceCodeType {
  Organizations = 1,
  Suppliers = 2,
  RepairShops = 3,
  IndividualOthers = 4,
  LocationsStores = 5
}

export enum EmployeeType {
  Prospective = 1,
  Active = 2,
  Terminated = 3
}

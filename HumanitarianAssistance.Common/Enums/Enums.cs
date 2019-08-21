namespace HumanitarianAssistance.Common.Enums
{
    public enum SwaggerGrouping
    {
        Accounting = 1,
        Configuration = 2,
        Hr = 3,
        Store = 4,
        Project = 5,
        Marketing = 6
    }



    public enum BalanceType
    {
        SUM = 1,
        DR = 2,
        CR = 3
    }
    public enum AccountTypeEnum
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
        Credit = 1,
        Debit = 2
    }

    public enum VoucherTypes
    {
        Journal = 1,
        Adjustment = 2
    }

    public enum InventoryMasterType
    {
        Consumables = 1,
        Expendables = 2,
        NonExpendables = 3
    }

    public enum SourceCode// start from here 
    {
        Organizations = 1,
        Suppliers = 2,
        RepairShops = 3,
        IndividualOthers = 4,
        LocationsStores = 5,
        Test = 6,
    }

    public enum ProjectPhaseType
    {
        Planning = 1,
        Implementation = 2,
        Completed = 3
    }
    public enum RoleName
    {
        HR = 1,
        AM = 2,
        SA = 3,
        AD = 4,
        PM = 5
    }
    public enum AccountLevelLimits
    {
        MainLevel = 9,
        ControlLevel = 99,
        SubLevel = 999,
        InputLevel = 999999
    }

    public struct Weekdays
    {
        public const string Monday = "Monday";
        public const string Tuesday = "Tuesday";
        public const string Wednesday = "Wednesday";
        public const string Thursday = "Thursday";
        public const string Friday = "Friday";
        public const string Saturday = "Saturday";
        public const string Sunday = "Sunday";
    }

}

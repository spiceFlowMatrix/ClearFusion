using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HumanitarianAssistance.Common.Enums {
    public enum SwaggerGrouping {
        Accounting = 1,
        Configuration = 2,
        Hr = 3,
        Store = 4,
        Project = 5,
        Marketing = 6,
        PdfExport = 7,
        StorePurchase = 8,
        VehicleTracker = 9,
        GeneratorTracker = 10
    }

    public enum BalanceType {
        SUM = 1,
        DR = 2,
        CR = 3
    }

    public enum UserStatus {
        InActive = 0,
        Active = 1
    }

    public enum AccountTypeEnum {

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

    public enum FinancialReportType {
        BALANCESHEET = 1,
        INCOMEANDEXPANCE = 2
    }

    public enum Gender {
        MALE = 1,
        FEMALE = 2,
        OTHER = 3
    }

    public enum AttendanceType {
        P = 1,
        A = 2,
        L = 3,
        H = 4
    }

    public enum ApplyLeaveStatus {
        Approve = 1,
        Reject = 2
    }

    public enum EmployeeTypeStatus {
        Prospective = 1,
        Active = 2,
        Terminated = 3
    }

    public enum HolidayType {
        PARTICULARDATE = 1,
        REPEATWEEKLYDAY = 2
    }

    public enum SalaryHeadType {
        ALLOWANCE = 1,
        DEDUCTION = 2,
        GENERAL = 3
    }

    public enum RECORDTYPE {
        SINGLE = 1,
        CONSOLIDATE = 2
    }

    public enum Sex {
        Male = 1,
        Female = 2,
        Other = 3
    }

    public enum LoggerEnum {
        VoucherCreated = 1,
        VoucherUpdate = 2,
        VoucherDeleted = 3,
        EmployeeCreated = 4,
        EmployeeUpdate = 5,
        EmployeeDeleted = 6
    }

    public enum Currency {
        AFG = 1,
        EUR = 2,
        PKR = 3,
        USD = 4
    }

    public enum AccountLevels {
        MainLevel = 1,
        ControlLevel = 2,
        SubLevel = 3,
        InputLevel = 4
    }
    public enum TransactionType {
        Credit = 1,
        Debit = 2
    }

    public enum VoucherTypes {
        Journal = 1,
        Adjustment = 2
    }

    public enum InventoryMasterType {
        Consumables = 1,
        Expendables = 2,
        NonExpendables = 3
    }

    public enum SourceCode // start from here 
    {
        Organizations = 1,
        Suppliers = 2,
        RepairShops = 3,
        IndividualOthers = 4,
        LocationsStores = 5,
        Test = 6,
    }

    public enum ProjectPhaseType {
        Planning = 1,
        Implementation = 2,
        Completed = 3
    }
    public enum RoleName {
        HR = 1,
        AM = 2,
        SA = 3,
        AD = 4,
        PM = 5
    }
    public enum AccountLevelLimits {
        MainLevel = 9,
        ControlLevel = 99,
        SubLevel = 999,
        InputLevel = 999999
    }

    public struct Weekdays {
        public const string Monday = "Monday";
        public const string Tuesday = "Tuesday";
        public const string Wednesday = "Wednesday";
        public const string Thursday = "Thursday";
        public const string Friday = "Friday";
        public const string Saturday = "Saturday";
        public const string Sunday = "Sunday";
    }

    public enum FileSourceEntityTypes {
        Voucher = 1,
        Account = 2,
        ProjectDetail = 3,
        ProjectProposal = 4,
        ProjectProposalSupportingDoc = 5,
        ProjectActivityImplementation = 6,
        ProjectActivityMonitoring = 7,
        ProjectActivityPlanning = 8,
        DonorDetail = 9,
        StorePurchase = 10,
        EmployeeProfile = 11,
        ComparativeStatement = 12,
        HiringRequestCandidateCV = 13,
        ProjectLogisticPurchase = 14,
        GoodsRecievedDocument = 15,
        LogisticSupplierInvoice = 16,
        LogisticSupplierWarranty = 17, [Description ("Proposal Document")]
        TenderProposalDocument = 18, [Description ("RFP Document")]
        TenderRFPDocument = 19, [Description ("Announcement Document")]
        TenderAnnouncementDocument = 20,
        TenderBidContractLetter = 21
    }

    public enum DocumentFileTypes {
        PurchaseImage = 1,
        PurchaseInvoice = 2,
        EmployeeProfile = 3
    }

    public enum TimeInterval {
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        Yearly = 4,
        Quarterly = 5
    }
    public enum OpportunityType {
        EOI = 1,
        Concept = 2,
        RFX = 3
    }

    public enum MarkedScores {
        Poor = 0,
        Fair = 1,
        Good = 2,
        Excellent = 3,
        Perfect = 4,
    }

    public enum QuestionType {
        Qualitative = 1,
        Quantitative = 2
    }

    public enum TransportItem {
        VehicleFuel = 1,
        GeneratorFuel = 2,
        VehicleMobilOil = 3,
        GeneratorMobilOil = 4,
        VehicleMaintenanceService = 5,
        GeneratorMaintenanceService = 6,
        VehicleSpareParts = 9,
        GeneratorSpareParts = 10,
    }

    public enum LogisticRequestStatus {
        [Description ("New Request")]
        NewRequest = 1, [Description ("Cancelled")]
        Cancelled = 2, [Description ("Issue Purchase Order")]
        IssuePurchaseOrder = 3, [Description ("Purchase Submitted")]
        CompletePurchase = 4, [Description ("Control Approved")]
        ControlApproved = 5, [Description ("Control Rejected")]
        ControlRejected = 6, [Description ("Purchase Completed")]
        PurchaseCompleted = 7
    }
    public enum LogisticComparativeStatus {
        [Description ("Pending")]
        Pending = 1, [Description ("Cancelled")]
        Cancelled = 2, [Description ("Issued")]
        Issued = 3, [Description ("Statement Submitted")]
        StatementSubmitted = 4, [Description ("Statement Rejected")]
        RejectStatement = 5, [Description ("Statement Approved")]
        ApproveStatement = 6,
    }

    public enum LogisticTenderStatus {
        [Description ("Pending")]
        Pending = 1, [Description ("Cancelled")]
        Cancelled = 2, [Description ("Issued")]
        Issued = 3, [Description ("Bid Selected")]
        BidSelected = 4,
    }
    //#region "StoreItemGroups"
    public enum TransportItemTypes {
        Vehicle = 1,
        Generator = 2
    }

    public enum Month {
        NotSet = 0,
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public enum HiringRequestStatus {
        Open = 1,
        InProgress = 2,
        Completed = 3,
        Closed = 4
    }
    public enum TransportItemCategory {
        Vehicle = 1,
        Generator = 2,
        MobilOil = 3,
        MaintenanceService = 4,
        SpareParts = 5,
        Fuel = 6
    }

    public enum UsageType {
        [Description ("Total Fuel Usage")]
        TotalFuelUsage = 1, [Description ("Current Mileage")]
        CurrentMileage = 2, [Description ("Actual Fuel Consumption Rate")]
        ActualFuelConsumptionRate = 3, [Description ("Fuel Consumption Difference")]
        FuelConsumptionDifference = 4, [Description ("Total Mobil Oil Usage")]
        TotalMobilOilUsage = 5, [Description ("Remaining Km For Mobil Oil Change")]
        RemainingKmForMobilOilChange = 6, [Description ("Mobil Oil Change Rotation")]
        MobilOilChangeRotation = 7, [Description ("Current Usage")]
        CurrentUsage = 8, [Description ("Actual Mobil Oil Consumption Rate")]
        ActualMobilConsumptionRate = 9, [Description ("Mobil Oil Consumption Difference")]
        MobilOilConsumptionDifference = 10,
    }

    public enum CostAnalysis {
        [Description ("Fuel Total Cost")]
        FuelTotalCost = 100, [Description ("Mobil Oil Total Cost")]
        MobilOilTotalCost = 101, [Description ("Spare Parts Total Cost")]
        SparePartsTotalCost = 102, [Description ("Service And Maintenance Total Cost")]
        ServiceAndMaintenanceTotalCost = 103,
    }
    public enum CandidateStatus {
        PendingShortlist = 0,
        PendingInterview = 1,
        PendingSelection = 2,
        Selected = 3,
        Rejected = 4
    }

    public enum RatingAction {
        Poor = 1,
        Good = 2,
        VeryGood = 3,
        Excellent = 4
    }
    public enum MaritalStatus {
        Single = 1,
        Married = 2,
        Divorced = 3,
        Widow = 4

    }

    public enum AccumulatedSalaryHead {

        Pension = 1,
        GrossSalary = 2,
        SalaryTax = 3,
        AdvanceRecovery = 4,
        CapacityBuilding = 5,
        Security = 6
    }

    public enum ResignationQuestionType {
        [Description ("Feeling About Employee Aspects")]
        FeelingAboutEmployeeAspects = 1, [Description ("Reason Of Leaving")]
        ReasonOfLeaving, [Description ("The Department")]
        TheDepartment, [Description ("The Job Itself")]
        TheJobItself, [Description ("My Supervisor")]
        MySupervisor, [Description ("The Management")]
        TheManagement
    }

    public enum ResignationStatus {
        NotValid = 0,
        Resigned = 1,
        ResignationRevoked = 2,
        Rehired = 3
    }


    public enum AppriasalStorngWeakPointType {
        Strong = 1,
        Weak = 2
    }

    public enum ActionType {

        Add = 0,
        Update = 1,
        Delete = 2,
        Download = 3
    }

    public enum TypeOfEntity {
        [Description ("This Action Perform on Employee History")]
        History = 0, [Description ("This Action Perform on Employee Leave")]
        Leave = 1, [Description ("This Action Perform on Employee Attendance")]
        Attendance = 2, [Description ("This Action Perform on Employee Contract")]
        Contract = 3, [Description ("This Action Perform on Employee TaxAndPension")]
        TaxAndPension = 4, [Description ("This Action Perform on Employee Salary Configuration")]
        SalaryConfiguration = 5, [Description ("This Action Perform on Employee Resignation")]
        Resignation = 6, [Description ("This Action Perform on Employee Advance")]
        Advance = 7, [Description ("This Action Perform on Employee Analytical Information")]
        AnalyticalInformation = 8

    }

    public enum OperationType
    {
        Store = 1,
        DirectVoucher = 2,
        Logistics = 3,
    }
}
export class EmployeePaymentDetail {
    //Employee Details
    EmployeeId: number;
    EmployeeName: string;
    EmployeeCode: string;
    PresentHours:number;

    //Currency
    CurrencyId: number;
    CurrencyCode: string;
    CurrencyName: string;

    //primary
    NetSalary: number;
    AdvanceRecoveryAmount: number; //Advance that will cut from the salary
    PensionAmount: number;
    SalaryTax: number;
    GrossSalary: number;

    HourlyRate: number;
    PensionRate: number;
    PaymentType: number;

    // Total Counts
    TotalAllowance: number;
    TotalDeduction: number;
    TotalGeneralAmount: number; //Basic Pay
    TotalWorkHours: number;

    EmployeePayrollListPrimary: EmployeePayrollList[];
    EmployeePayrollList: EmployeePayrollList[];
}

class EmployeePayrollList {
    CurrencyId: number;
    EmployeeId: number;

    MonthlyAmount: number;
    SalaryHead: string; //name
    SalaryHeadId: number;
    HeadTypeId: number;//Allowance,Deduction,General
    SalaryHeadType: string;//Allowance,Deduction,General
    PensionRate: number;

    AccountNo: number;
    AccountName: string;
    TransactionTypeId: number;
    TransactionTypeName: string;
}


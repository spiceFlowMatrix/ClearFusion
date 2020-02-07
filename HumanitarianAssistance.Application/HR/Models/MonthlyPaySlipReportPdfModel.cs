using System;

namespace HumanitarianAssistance.Application.HR.Models {
    public class MonthlyPaySlipReportPdfModel {
        public DateTime PaymentDate { get; set; }
        public string SalaryMonth { get; set; }
        public string SalaryYear { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Type { get; set; }
        public string Office { get; set; }
        public string Sex { get; set; }

        public string BudgetLine { get; set; }
        public string Program { get; set; }
        public string Project { get; set; }
        public string Job { get; set; }
        public string Sector { get; set; }
        public string Area { get; set; }
        public string Account { get; set; }
        public string SalaryPercentage { get; set; }
        public string AnalyticalSalary { get; set; }

        public string BasicSalary { get; set; }
        public string CurrencyCode { get; set; }
        public string Attendance { get; set; }
        public string Absentess { get; set; }
        public string Salary { get; set; }

        public string Food { get; set; }
        public string Tr { get; set; }
        public string Medical { get; set; }
        public string AllowanceOther { get; set; }
        public string AllowanceOther1 { get; set; }
        public string AllowanceOther2 { get; set; }
        public string GrossSalary { get; set; }

        public string Advance { get; set; }
        public string SalaryTax { get; set; }
        public string Fine { get; set; }
        public string DeductionOther { get; set; }
        public string Pension { get; set; }
        public string Cb { get; set; }
        public string Security { get; set; }
        public string Net { get; set; }
        public string AFN { get; set; }

        public string Other1Desc { get; set; }
        public string Other2Desc { get; set; }

        public string ApprovedEmployeeCode { get; set; }
        public string ApprovedEmployeeName { get; set; }
        public string ApprovedEmployeeSignature { get; set; }

        public string PersianChaName { get; set; }
        public string LogoPath { get; set; }
    }
}
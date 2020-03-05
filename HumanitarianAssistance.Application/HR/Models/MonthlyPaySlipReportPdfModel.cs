using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Models {
    public class MonthlyPaySlipReportPdfModel {
        public string PaymentDate { get; set; }
        public int? SalaryMonth { get; set; }
        public int? SalaryYear { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Type { get; set; }
        public string Office { get; set; }
        public string Sex { get; set; }

        public List<AnalyticalInfo> AnalyticalInfo { get; set; }

        public double? BasicSalary { get; set; }
        public string CurrencyCode { get; set; }
        public int Attendance { get; set; }
        public int Absentess { get; set; }
        public double? Salary { get; set; }

        public double? AllowanceAdvance { get; set; }
        public double? AllowanceSalaryTax { get; set; }
        public double? AllowancePension { get; set; }
        public double? AllowanceCb { get; set; }
        public double? AllowanceSecurity { get; set; }

        public double? GrossSalary { get; set; }

        public double? Advance { get; set; }
        public double? SalaryTax { get; set; }
        public double? Fine { get; set; }
        public double? Pension { get; set; }
        public double? Cb { get; set; }
        public double? Security { get; set; }
        public double? Net { get; set; }
        public string SalaryCurrencyCode { get; set; }

        public string ApprovedEmployeeCode { get; set; }
        public string ApprovedEmployeeName { get; set; }
        public string ApprovedEmployeeSignature { get; set; }

        public string PersianChaName { get; set; }
        public string LogoPath { get; set; }
    }

    public class AnalyticalInfo {
        public string BudgetLine { get; set; }
        public List<ProjectProgramDetail> Program { get; set; }
        public string Project { get; set; }
        public JobDetail Job { get; set; }
        public List<ProjectSectorDetail> Sector { get; set; }
        public string Area { get; set; }
        public string Account { get; set; }
        public string SalaryPercentage { get; set; }
        public double? AnalyticalSalary { get; set; }
    }
    public class ProjectSectorDetail {
        public string Sector { get; set; }
    }
    
    public class ProjectProgramDetail {
        public string Program { get; set; }
    }
    public class JobDetail {
        public string Job { get; set; }
    }
}
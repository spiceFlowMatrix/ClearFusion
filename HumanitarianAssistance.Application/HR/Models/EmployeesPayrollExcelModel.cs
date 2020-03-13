using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeesPayrollExcelModel
    {
        public EmployeesPayrollExcelModel()
        {
            HeaderAndFooter = new HeaderAndFooter();
            PayrollExcelData= new List<PayrollExcelData>();
        }

        public HeaderAndFooter HeaderAndFooter { get; set; }
        public List<PayrollExcelData> PayrollExcelData { get; set; }
    }

    public class PayrollExcelData 
    {
        public PayrollExcelData()
        {
            EmployeeAnalyticalInfoList = new List<EmployeeAnalyticalInfo>();
        }
        
        public int EmployeeId { get; set; }
        public int? CurrencyId { get; set; }
        public int? OfficeId { get; set; }
        public string Name { get; set; }
        public string EmployeeCode { get; set; }
        public string Designation { get; set; }
        public string Gender { get; set; }
        public string Currency { get; set; }
        public string Office { get; set; }
        public double BasicPay { get; set; }
        public int AttendedHours { get; set; }
        public int AbsentHours { get; set; }
        public double Salary { get; set; }
        public double Bonus { get; set; }
        public double GrossSalary { get; set; }
        public double CapacityBuilding { get; set; }
        public double Security { get; set; }
        public double SalaryTax { get; set; }
        public double Fine { get; set; }
        public double Advance { get; set; }
        public double Pension { get; set; }
        public double NetSalary { get; set; }
        public string Month { get; set; }
        public string Project { get; set; }
        public string Job { get; set; }
        public string BudgetLine { get; set; }
        public double Percentage { get; set; }
        public double HourlyRate { get; set; }
        public int MonthNumber { get; set; }
        public int AbsentDays { get; set; }
        public long? AttendanceGroupId { get; set; }
        public List<EmployeeAnalyticalInfo> EmployeeAnalyticalInfoList { get; set; }
    }
    
    public class HeaderAndFooter
    {
        public string Header { get; set; }
        public string SubHeader { get; set; }
        public string Currency { get; set; }
        public string Office { get; set; }
        public string Months { get; set; }
    }

    public class EmployeeAnalyticalInfo 
    {
        public string Project { get; set; }
        public string Job { get; set; }
        public string BudgetLine { get; set; }
        public double Percentage { get; set; }
    }
}
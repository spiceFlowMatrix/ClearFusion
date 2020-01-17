using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeePensionReportPdfMdel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Currency { get; set; }
        public List<PensionReportListModel> PensionReportListModel { get; set; }
        public string LogoPath { get; set; }
        public double PensionDeductionTotal { get; set; }
        public double PensionProfitTotal { get; set; }
        public double Total { get; set; }

    }
    public class PensionReportListModel
    {
        public int Year { get; set; }
        public List<PensionList> PensionList { get; set; }
    }
    public class PensionList
    {
        public string Date { get; set; }
        public double GrossSalary { get; set; }
        public double PensionRate { get; set; }
        public double PensionDeduction { get; set; }
        public double Profit { get; set; }
        public double Total { get; set; }
    }
}
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class EmployeeLeaveReportPdfModel
    {
        public EmployeeLeaveReportPdfModel()
        {
            EmployeeLeaves = new List<EmployeeMonthLeavesModel>();
        }

        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public int Year { get; set; }
        public List<EmployeeMonthLeavesModel> EmployeeLeaves { get; set; }
    }
}
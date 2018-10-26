using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeeSalaryVoucherModel
    {
        //public decimal GrossSalary { get; set; } 
        //public decimal NetSalary { get; set; }
        public List<SalaryHeadModel> EmployeePayrollLists { get; set; }
        public List<PayrollHeadModel> EmployeePayrollListPrimary { get; set; }
        public string CreatedById { get; set; }
        public int OfficeId { get; set; }
        public int EmployeeId { get; set; }
        public int CurrencyId { get; set; }
        public int JournalCode { get; set; }
        public int PresentHours { get; set; }
    }
}

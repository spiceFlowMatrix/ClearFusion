using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Accounting.Models
{
    public class EmployeeSalaryVoucherModel
    {
        public List<SalaryHeadModel> EmployeePayrollLists { get; set; }
        public List<PayrollHeadModel> EmployeePayrollListPrimary { get; set; }
        public string CreatedById { get; set; }
        public int OfficeId { get; set; }
        public int EmployeeId { get; set; }
        public int CurrencyId { get; set; }
        public int JournalCode { get; set; }
        public int PresentHours { get; set; }
        public DateTime PayrollMonth { get; set; }
    }
}
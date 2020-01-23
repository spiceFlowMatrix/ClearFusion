using System.Collections.Generic;

namespace HumanitarianAssistance.Application.HR.Models
{
    public class PayrollModel
    {
        public PayrollModel()
        {
            AccumulatedPayrollHeadList = new List<AccumulatedPayrollHeads>();
        }

        public double GrossSalary { get; set; }
        public double NetSalary { get; set; }
        public double SalaryPaid { get; set; }
        public bool isSalaryApproved { get; set; }
        public string Status { get; set; }
        public List<AccumulatedPayrollHeads> AccumulatedPayrollHeadList {get; set;}
    }

    public class AccumulatedPayrollHeads
    {
        public int Id { get; set; }
        public string PayrollHeadName { get; set; }
        public double Amount { get; set; }
        public int TransactionType { get; set; }
    }
}
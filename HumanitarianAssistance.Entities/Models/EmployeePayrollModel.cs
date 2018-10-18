﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class EmployeePayrollModel : BaseModel
    {
        public long PayrollId { get; set; }
        public int HeadTypeId { get; set; }
        public int SalaryHeadId { get; set; }
        public int EmployeeId { get; set; }
        public int CurrencyId { get; set; }
        public int PaymentType { get; set; }
        public string SalaryHeadType { get; set; }
        public string SalaryHead { get; set; }
        public double MonthlyAmount { get; set; }
		public double? PensionRate { get; set; }
        public double? BasicPay { get; set; }
        public long? AccountNo { get; set; }
        public int? TransactionTypeId { get; set; }

        //public List<EmployeePayrollModel> employeepayrolllist { get; set; }
    }

    public class EmployeePayrollAccountModel : BaseModel
    {
        public int PayrollHeadId { get; set; }
        public int EmployeeId { get; set; }
        public int PayrollHeadTypeId { get; set; }
        public string PayrollHeadName { get; set; }
        public string Description { get; set; }
        public long? AccountNo { get; set; }
        public int? TransactionTypeId { get; set; }
        public string SalaryHeadType { get; set; }
    }


}

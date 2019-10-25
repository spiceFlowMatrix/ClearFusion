﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class SalaryHeadModel : BaseModel
    {
        public int SalaryHeadId { get; set; }
        public int HeadTypeId { get; set; }
        public string HeadName { get; set; }
        public string Description { get; set; }
        public long AccountNo { get; set; }
        public int? TransactionTypeId { get; set; }
        public decimal? MonthlyAmount { get; set; }
        public bool SaveForAll { get; set; }
    }

    public class PayrollHeadModel : BaseModel
    {
        public int PayrollHeadId { get; set; }
        public int PayrollHeadTypeId { get; set; }
        public string PayrollHeadName { get; set; }
        public string Description { get; set; }
        public long? AccountNo { get; set; }
        public int? TransactionTypeId { get; set; }
        public decimal? Amount { get; set; }
        public int? OfficeId { get; set; }
    }
}

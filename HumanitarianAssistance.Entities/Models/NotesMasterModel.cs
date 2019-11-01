﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class NotesMasterModel : BaseModel
    {
        public int NoteId { get; set; }
        public long AccountCode { get; set; }
        public string ChartOfAccountCode { get; set; }
        public string Narration { get; set; }
        public int Notes { get; set; }
        public int BlanceType { get; set; } // 1 = Sum , 2 = CR , 3 = DR
        public string BlanceTypeName { get; set; }
        public int FinancialReportTypeId { get; set; } // 1 = Balance Sheet , 2 = Income and Expance
        public string FinancialReportTypeName { get; set; }
        public int? AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }
        public double BalanceAmount { get; set; }
    }

    public class DetailsOfNotesModel
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
    }

    public class DetailsOfNotesFinalModel
    {
        public string NoteName { get; set; }
        public double TotalDebits { get; set; }
        public double TotalCredits { get; set; }
        public double Balance { get; set; }
        public List<DetailsOfNotesModel> AccountSummary { get; set; }
    }
}

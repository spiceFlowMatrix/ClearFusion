using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class BalanceSheetModel
    {
        //public string AccountType { get; set; }
        public string Narration { get; set; }
        public int Note { get; set; }
        public double Balance { get; set; }
    }

    public class BalanceSheet
    {
        public IList<BalanceSheetModel> CurrentAssest { get; set; }
        public IList<BalanceSheetModel> Libility { get; set; }
        public IList<BalanceSheetModel> Equity { get; set; }
        public IList<BalanceSheetModel> Revenue { get; set; }
        public IList<BalanceSheetModel> Expense { get; set; }
        public IList<BalanceSheetModel> Income { get; set; }
        public IList<BalanceSheetModel> Funds { get; set; }
        public IList<BalanceSheetModel> Reserve_Account_Adjustment { get; set; }
        public IList<BalanceSheetModel> Long_Term_Libility { get; set; }
        public IList<BalanceSheetModel> Current_Libility { get; set; }
        public IList<BalanceSheetModel> Reserve_Account { get; set; }
    }


}

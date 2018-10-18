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
        public double? Balance { get; set; }
		public int AccountTypeId { get; set; }
	}

    public class BalanceSheet
    {
        public IList<BalanceSheetModel> CapitalAssetsWrittenOff { get; set; }
        public IList<BalanceSheetModel> CurrentAssets { get; set; }
        public IList<BalanceSheetModel> Funds { get; set; }
        public IList<BalanceSheetModel> EndownmentFund { get; set; }
        public IList<BalanceSheetModel> ReserveAccountAdjustment { get; set; }
        public IList<BalanceSheetModel> LongtermLiability { get; set; }
        public IList<BalanceSheetModel> CurrentLiability { get; set; }
        public IList<BalanceSheetModel> ReserveAccount { get; set; }
        public IList<BalanceSheetModel> IncomeFromDonor { get; set; }
        public IList<BalanceSheetModel> IncomeFromProjects { get; set; }
        public IList<BalanceSheetModel> ProfitOnBankDeposits { get; set; }
		public IList<BalanceSheetModel> IncomeExpenditureFund { get; set; }
	}


}

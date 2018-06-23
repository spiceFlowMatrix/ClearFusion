using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class ExchangeGainOrLossModel
    {
		public TransactionsModel TransactionsModel { get; set; }	
		public double Total { get; set; }
	}

	public class TransactionsModel
	{
		public long ChartOfAccountCode { get; set; }
		public double OriginalAmount { get; set; }
		public double CurrentAmount { get; set; }
		public double Balance { get; set; }
	}
}

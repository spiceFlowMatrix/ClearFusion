using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class ExchangeGainOrLossFilterModel
    {
		public List<int> AccountCodes { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int? AccountCode { get; set; }
	}
}

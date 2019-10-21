using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.SPModels
{
    public class SP_ExchangeRate
    {
        public int OfficeId { get; set; }
        public DateTime Date { get; set; }
        public int FromCurrency { get; set; }
        public double Rate { get; set; }
      
    }
}

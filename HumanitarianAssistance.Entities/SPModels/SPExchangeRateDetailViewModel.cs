﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.SPModels
{
   public class SPExchangeRateDetailViewModel
    {

        public int ToCurrency { get; set; }
        public int FromCurrency { get; set; }
        public double Rate { get; set; }
    }
}

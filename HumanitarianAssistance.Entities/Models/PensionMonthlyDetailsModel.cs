using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class PensionMonthlyDetailsModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public double? GrossSalary { get; set; }
        public double PensionRate { get; set; }
        public double PensionDeduction { get; set; }
        public double Profit { get; set; }
        public double Total { get; set; }

    }
}

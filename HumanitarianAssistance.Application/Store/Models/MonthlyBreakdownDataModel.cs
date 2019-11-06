using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class MonthlyBreakdownDataModel
    {
        public MonthlyBreakdownDataModel()
        {
            MonthlyBreakDownList = new List<MonthlyBreakDown>();
        }

        public double IncurredMileage { get; set; }
        public double StartingMileage { get; set; }
        public double StandardFuelConsumptionRate { get; set; }
        public double StandardMobilOilConsumptionRate { get; set; }
        public double StartingCost { get; set; }
        public List<MonthlyBreakDown> MonthlyBreakDownList { get; set; }
    }

    public class MonthlyBreakDown
    {
        public string Header { get; set; }
        public double January { get; set; }
        public double February { get; set; }
        public double March { get; set; }
        public double April { get; set; }
        public double May { get; set; }
        public double June { get; set; }
        public double July { get; set; }
        public double August { get; set; }
        public double September { get; set; }
        public double October { get; set; }
        public double November { get; set; }
        public double December { get; set; }
    }
}
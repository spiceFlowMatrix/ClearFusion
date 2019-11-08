using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class MonthlyBreakdownDataModel
    {
        public MonthlyBreakdownDataModel()
        {
            UsageAnalysisBreakDownList = new List<UsageAnalysisBreakDown>();
            CostAnalysisBreakDownList= new List<CostAnalysisBreakDown>();
        }

        public double IncurredMileage { get; set; }
        public double StartingMileage { get; set; }
        public double IncurredUsage{ get; set; }
        public double StartingUsage { get; set; }
        public double StandardFuelConsumptionRate { get; set; }
        public double StandardMobilOilConsumptionRate { get; set; }
        public double StartingCost { get; set; }
        public List<UsageAnalysisBreakDown> UsageAnalysisBreakDownList { get; set; }
        public List<CostAnalysisBreakDown> CostAnalysisBreakDownList { get; set; }
    }

    public class UsageAnalysisBreakDown
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

    public class CostAnalysisBreakDown
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
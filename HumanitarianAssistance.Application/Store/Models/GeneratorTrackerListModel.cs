using System.Collections.Generic;
using System;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class GeneratorTrackerListModel
    {
        public GeneratorTrackerListModel()
        {
            GeneratorTrackerList= new List<GeneratorTrackerModel>();
        }
        public int TotalRecords {get; set;}
        public List<GeneratorTrackerModel> GeneratorTrackerList {get; set;}
        
    }
    public class GeneratorTrackerModel 
    {
        public long GeneratorId { get; set; }
        public double Voltage { get; set; }
        public double StartingUsage { get; set; }
        public double IncurredUsage { get; set; }
        public double TotalUsage { get; set; }
        public double TotalCost { get; set; }
        public double OriginalCost { get; set; }
        public double FuelConsumptionRate { get; set; }
        public double MobilOilConsumptionRate { get; set; }
        public int OfficeId { get; set; }
        public int ModelYear { get; set; }
        public long PurchaseId { get; set; }
        public int CurrencyId { get; set; }
        public DateTime PurchasedDate {get; set;}
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class DepreciationReportModel
    {
        public string ItemName { get; set; }
        public string PurchaseId { get; set; }
        public double HoursSincePurchase { get; set; }
        public double DepreciationRate { get; set; }
        public double DepreciationAmount { get; set; }
        public double CurrentValue { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double PurchasedCost { get; set; }
    }
}

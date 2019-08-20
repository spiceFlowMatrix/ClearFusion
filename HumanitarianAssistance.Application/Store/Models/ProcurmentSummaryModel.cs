using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class ProcurmentSummaryModel
    {
        public ProcurmentSummaryModel()
        {
            TotalCostDetails = new TotalCostDetails();
        }
        public string ProcurementId { get; set; }           // Order ID
        public DateTime ProcurementDate { get; set; }       // IssueDate
        public string EmployeeName { get; set; }
        public int? Store { get; set; }                      // Names in front-end because there is no master for it
        public string Inventory { get; set; }               // Inventory Name
        public string Item { get; set; }                    // Item Name
        public decimal? TotalCost { get; set; }
        public string MustReturn { get; set; }
        public string Returned { get; set; }
        public DateTime VoucherDate { get; set; }
        public long? VoucherNo { get; set; }
        public TotalCostDetails TotalCostDetails { get; set; }
        public int CurrencyId { get; set; }
    }
    public class TotalCostDetails
    {
        public string UnitType { get; set; }                // Unit Type Name
        public int Amount { get; set; }                     // Quantity Purchase table Name
        public decimal UnitCost { get; set; }                  // Purchase Table
        public string Currency { get; set; }                // Currency Name
    }
}

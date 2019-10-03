using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class PurchaseListModel
    {
        public PurchaseListModel()
        {
            ProcurementList= new List<ProcurementListModel>();
        }

        public long PurchaseId { get; set; }
        public long ItemId { get; set; }
        public int CurrencyId { get; set; }
        public string ItemName { get; set; }
        public string EmployeeName { get; set; }
        public long? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public double OriginalCost { get; set; }
        public double DepreciatedCost { get; set; }
        public DateTime PurchaseDate {get; set;}
        public int recordsCount { get; set; }
        public List<ProcurementListModel> ProcurementList { get; set; }
    }
}
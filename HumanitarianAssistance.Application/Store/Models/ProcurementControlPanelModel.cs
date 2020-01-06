using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class ProcurementControlPanelModel
    {
        public long Id { get; set; }
        public long PurchaseId { get; set; }
        public int StartingBalance { get; set; }
        public int CurrentBalance { get; set; }
        public bool MustReturn { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string ItemCode { get; set; }
        public string ProjectName {get; set;}  
        public string VoucherNo { get; set; }
        public string EmployeeName { get; set; }  
        public long ProjectId { get; set; }

        public List<ProcurementReturnModel> ProcurementReturnList { get; set; }

    }

    public class ProcurementReturnModel 
    {
        public long Id { get; set; }
        public string Date { get; set; }
        public int ReturnedQuantity  { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class ReturnProcurementDetail: BaseEntity
    {
        public long Id { get; set; }
        public DateTime ReturnedDate {get; set;}
        public int ReturnedQuantity { get; set; }
        public long ProcurementId {get; set; }
        [ForeignKey("ProcurementId")]
        public StorePurchaseOrder StorePurchaseOrder { get; set; }
    }
}
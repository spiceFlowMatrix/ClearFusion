using System;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class ReturnProcurementDetail: BaseEntity
    {
        public long Id { get; set; }
        public DateTime ReturnedDate {get; set;}
        public int ReturnedQuantity { get; set; }
    }
}
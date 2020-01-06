using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddProcurementReturnCommand: BaseModel, IRequest<object>
    {
        public long PurchaseId { get; set; }
        public long ProcurementId { get; set; }
        public DateTime ReturnedDate { get; set; }
        public int ReturnedQuantity { get; set; }
    }
}
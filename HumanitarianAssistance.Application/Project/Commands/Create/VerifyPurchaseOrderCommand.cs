using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class VerifyPurchaseOrderCommand: BaseModel, IRequest<ApiResponse>
    {
        public long RequestId { get; set; }
        public int Journal { get; set; }
        public string VoucherDescription { get; set; }
        public long CreditAccount { get; set; }
        public string CreditDescription { get; set; }
        public long DebitAccount { get; set; }
        public string DebitDescription { get; set; }
        public double TotalCost { get; set; }
    }
}
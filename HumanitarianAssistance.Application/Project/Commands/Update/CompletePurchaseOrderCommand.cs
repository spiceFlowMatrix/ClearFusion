using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class CompletePurchaseOrderCommand : BaseModel, IRequest<ApiResponse>
    {
        public List<CompletePurchaseOrderModel> submittedList { get; set; }
        public long Status { get; set; }
    }

    public class CompletePurchaseOrderModel
    {
        public long Id { get; set; }
        public double FinalCost { get; set; }
    }
}
 
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class CompletePurchaseOrderCommand : BaseModel, IRequest<ApiResponse>
    {
        public long[] Id { get; set; }
        public long Status { get; set; }
    }
}
 
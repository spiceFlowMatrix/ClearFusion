using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeletePurchaseCommand : BaseModel, IRequest<ApiResponse>
    {
        public string PurchaseId { get; set; }
    }
}

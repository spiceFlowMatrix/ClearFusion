using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeletePaymentTypesCommand : BaseModel, IRequest<ApiResponse>
    {
        public int PaymentId { get; set; } 
    }
}

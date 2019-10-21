using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddPaymentTypesCommand : BaseModel, IRequest<ApiResponse>
    {
        public int PaymentId { get; set; }
        public string Name { get; set; }
        public long ChartOfAccountNewId { get; set; } 
    }
}

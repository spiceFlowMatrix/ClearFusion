using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditPaymentTypesCommand : BaseModel, IRequest<ApiResponse>
    {
        public int PaymentId { get; set; }
        public string Name { get; set; }
        public long ChartOfAccountNewId { get; set; }
    }
}

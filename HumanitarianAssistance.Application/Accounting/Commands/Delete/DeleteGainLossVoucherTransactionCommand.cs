using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Delete
{
    public class DeleteGainLossVoucherTransactionCommand: BaseModel, IRequest<ApiResponse>
    {
        public long VoucherNo {get; set;}
    }
}
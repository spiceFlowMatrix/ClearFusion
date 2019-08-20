using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllTransactionsByVoucherIdQuery : IRequest<ApiResponse>
    {
        public long VoucherId { get; set; }
    }
}
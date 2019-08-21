using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetVoucherDetailByVoucherNoQuery : IRequest<ApiResponse>
    {
        public long VoucherId { get; set; }
    }
}
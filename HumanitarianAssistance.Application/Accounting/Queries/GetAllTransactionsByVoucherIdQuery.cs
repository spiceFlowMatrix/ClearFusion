using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllTransactionsByVoucherIdQuery : IRequest<ApiResponse>
    {
        public long VoucherNo { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetVoucherTransactionListQuery: IRequest<ApiResponse>
    {
        public int VoucherNo { get; set; }
        public int CurrencyId { get; set; }
        public int RecordType { get; set; }
    }
}
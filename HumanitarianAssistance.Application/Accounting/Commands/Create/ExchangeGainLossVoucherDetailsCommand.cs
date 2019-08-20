using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Create
{
    public class ExchangeGainLossVoucherDetailsCommand : BaseModel, IRequest<ApiResponse>
    {
        public long VoucherNo { get; set; }
        public int CurrencyId { get; set; }
        public string Description { get; set; }
        public int? JournalId { get; set; }
        public int? VoucherType { get; set; }
        public int? OfficeId { get; set; }
        public long? ProjectId { get; set; }
        public long? BudgetLineId { get; set; }
        public long CreditAccount { get; set; }
        public long DebitAccount { get; set; }
        public double Amount { get; set; }
        public int? TimezoneOffset { get; set; }
    }
}
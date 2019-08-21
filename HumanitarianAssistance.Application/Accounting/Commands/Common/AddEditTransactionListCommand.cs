using MediatR;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.CommonModels;

namespace HumanitarianAssistance.Application.Accounting.Commands.Common
{
    public class AddEditTransactionListCommand : BaseModel, IRequest<ApiResponse>
    {
        public AddEditTransactionListCommand()
        {
            VoucherTransactions = new List<VoucherTransactionsModel>();
        }
        public long VoucherNo { get; set; }
        public List<VoucherTransactionsModel> VoucherTransactions { get; set; }
    }
}
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;

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
        public int? TimezoneOffset { get; set; }
        public DateTime VoucherDate { get; set; }
        public List<TransactionListModel> TransactionList { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class TransactionListModel
    {
        public long AccountId { get; set; }
        public string Account { get; set; }
        public double  CreditAmount { get; set; }
        public double  DebitAmount { get; set; }
        public string Description { get; set;} 
    }
}
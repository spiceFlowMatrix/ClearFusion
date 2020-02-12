using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Create
{
    public class SaveTransactionListCommand: BaseModel, IRequest<object>
    {
        public SaveTransactionListCommand()
        {
            TransactionList = new List<TransactionModel>();
        }

        public List<TransactionModel> TransactionList { get; set; }
        public long VoucherNo { get; set; }
    }
    
    public class TransactionModel
    {
        public long? AccountNo { get; set; }
        public double? Debit { get; set; }
        public double? Credit { get; set; }
        public long? BudgetLineId { get; set; }
        public long? ProjectId { get; set; }
        public string Description { get; set; }
         public long TransactionId { get; set; }
         public long? VoucherNo { get; set; }
         public long? JobId { get; set; }
         public int? Type { get; set; }
    }
}
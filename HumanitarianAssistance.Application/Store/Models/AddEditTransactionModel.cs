using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.CommonModels;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class AddEditTransactionModel
    {
        public AddEditTransactionModel()
        {
            VoucherTransactions = new List<VoucherTransactionsModel>();
        }
        public long VoucherNo { get; set; }
        public List<VoucherTransactionsModel> VoucherTransactions { get; set; }

    }
}

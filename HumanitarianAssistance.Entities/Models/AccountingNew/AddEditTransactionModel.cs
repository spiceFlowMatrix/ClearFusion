using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.AccountingNew
{
    public class AddEditTransactionModel
    {
        public AddEditTransactionModel() {
            VoucherTransactions = new List<VoucherTransactionsModel>();
        }
        public long VoucherNo { get; set; }
        public List<VoucherTransactionsModel> VoucherTransactions { get; set; }

    }
}

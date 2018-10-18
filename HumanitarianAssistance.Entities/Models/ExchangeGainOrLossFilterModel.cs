using HumanitarianAssistance.ViewModels.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models
{
    public class ExchangeGainOrLossFilterModel : PaginationModel
    {

        public int OfficeId { get; set; }
        public int JournalId { get; set; }
        public List<long?> VoucherList { get; set; }
        public List<int?> AccountList { get; set; }
        public List<int?> ProjectList { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? DateOfComparison { get; set; }
        public int ComparisonCurrencyId { get; set; }


        //    public List<int?> AccountCodes { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        //public int? AccountCode { get; set; }
        //public int? OfficeId { get; set; }


    }

    public class ExchangeGainOrLossTransactionFilterModel : PaginationModel
    {

        public int? OfficeId { get; set; }
        public List<long?> AccountList { get; set; }
        public int ExchangeGainLossAccount { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? DateOfComparison { get; set; }
        public int ComparisonCurrencyId { get; set; }
        public int TransactionType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetExchangeGainLossReportQuery : IRequest<ApiResponse>
    {
        public GetExchangeGainLossReportQuery()
        {

            OfficeIdList = new List<int?>();
            JournalIdList = new List<int?>();
            ProjectIdList = new List<long?>();
            AccountIdList = new List<long?>();
        }

        public int ToCurrencyId { get; set; }
        public DateTime ComparisionDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public List<int?> OfficeIdList { get; set; }
        public List<int?> JournalIdList { get; set; }
        public List<long?> ProjectIdList { get; set; }
        public List<long?> AccountIdList { get; set; }
    }
}
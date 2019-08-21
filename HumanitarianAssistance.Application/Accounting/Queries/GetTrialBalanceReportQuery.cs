using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetTrialBalanceReportQuery: IRequest<ApiResponse>
    {
        public List<int> OfficesList { get; set; }
        public List<int> OfficeIdList { get; set; }
        public int CurrencyId { get; set; }
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        public int RecordType { get; set; }
        public List<long> accountLists { get; set; }
        
    }
}
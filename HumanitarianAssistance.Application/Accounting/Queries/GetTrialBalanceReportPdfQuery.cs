using System;
using HumanitarianAssistance.Application.Infrastructure;
using System.Collections.Generic;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{    
    public class GetTrialBalanceReportPdfQuery : IRequest<byte[]>
    {
        public List<int> OfficesList { get; set; }
        public int CurrencyId { get; set; }
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        public int RecordType { get; set; }
        public List<long> accountLists { get; set; }
    }
}
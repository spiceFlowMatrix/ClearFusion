using System;
using HumanitarianAssistance.Application.Infrastructure;
using System.Collections.Generic;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{    
    public class GetJournalLedgerReportPdfQuery : IRequest<byte[]>
    {
        public int CurrencyId { get; set; }
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        public List<int> OfficesList { get; set; }
        public int RecordType { get; set; }
        public List<int> JournalCode { get; set; }
        public List<long> AccountLists { get; set; }
        public List<long> Project { get; set; }
        public List<long> BudgetLine { get; set; }
        public List<long> JobCode { get; set; }
    }
}
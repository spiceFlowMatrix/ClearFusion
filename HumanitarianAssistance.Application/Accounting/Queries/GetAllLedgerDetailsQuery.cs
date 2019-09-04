using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllLedgerDetailsQuery:IRequest<ApiResponse>
    {
        public GetAllLedgerDetailsQuery()
        {
            OfficeIdList = new List<int>();
            accountLists = new List<long>();
        }
        public List<int> OfficeIdList { get; set; }
        public int CurrencyId { get; set; }
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        public int RecordType { get; set; }
        public List<long> accountLists { get; set; }
    }
}

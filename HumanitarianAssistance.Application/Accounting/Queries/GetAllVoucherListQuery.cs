using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllVoucherListQuery : IRequest<ApiResponse>
    {
        public string FilterValue { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CurrencyId { get; set; }
        public int? OperationalType { get; set; }
        public int? JournalId { get; set; }
        public List<int?> OfficeIds { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? totalCount { get; set; }
    }
}
using System.Collections.Generic;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetVoucherSummaryReportListQuery : PagingModel, IRequest<ApiResponse>
    {
        public List<long> Accounts { get; set; }
        public List<long> BudgetLines { get; set; }
        public int Currency { get; set; }
        public List<int> Journals { get; set; }
        public List<int> Offices { get; set; }
        public List<long> ProjectJobs { get; set; }
        public List<long> Projects { get; set; }
        public int RecordType { get; set; }

    }
}
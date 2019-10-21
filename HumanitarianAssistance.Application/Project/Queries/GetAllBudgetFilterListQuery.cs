using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllBudgetFilterListQuery : IRequest<ApiResponse>
    {
        public string FilterValue { get; set; }
        public bool BudgetLineIdFlag { get; set; }
        public bool BudgetCodeFlag { get; set; }
        public bool BudgetNameFlag { get; set; }
        public bool ProjectJobIdFlag { get; set; }
        public bool InitialBudgetFlag { get; set; }
        public bool DateFlag { get; set; }
        public bool ProjectJobNameFlag { get; set; }


        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public int? totalCount { get; set; }
        public long? ProjectId { get; set; }
    }
}

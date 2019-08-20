using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class FilterBudgetLineBreakdownQuery : IRequest<ApiResponse>
    {
        FilterBudgetLineBreakdownQuery()
        {
            BudgetLineId = new List<long>();
        }

        public long ProjectId { get; set; }
        public List<long> BudgetLineId { get; set; }
        public DateTime BudgetLineStartDate { get; set; }
        public DateTime BudgetLineEndDate { get; set; }
        public int CurrencyId { get; set; }
    }
}

using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectJobsByMultipleBudgetLineIdsQuery : IRequest<ApiResponse>
    {
        public List<long?> budgetLineIds { get; set; }
    }
}
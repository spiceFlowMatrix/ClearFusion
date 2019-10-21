using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetBudgetLinesByMultipleProjectJobIdsQuery : IRequest<ApiResponse>
    {
        public List<long?> projectJobIds { get; set; }
    } 
}

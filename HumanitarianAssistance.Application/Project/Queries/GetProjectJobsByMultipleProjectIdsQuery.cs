using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectJobsByMultipleProjectIdsQuery : IRequest<ApiResponse>
    {
        public List<long> projectIds { get; set; }
    }
}

using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllExistingCandidateListQuery: IRequest<ApiResponse>
    {      
        public long ProjectId { get; set; }
        public long HiringRequestId { get; set; }
        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public int? TotalCount { get; set; }  
        public string FilterValue { get; set; }  
    }
}
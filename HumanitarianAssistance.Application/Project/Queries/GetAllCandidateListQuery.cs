using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllCandidateListQuery: IRequest<ApiResponse>
    {      
        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public int? TotalCount { get; set; }  
        public string FilterValue { get; set; }  
    }
}
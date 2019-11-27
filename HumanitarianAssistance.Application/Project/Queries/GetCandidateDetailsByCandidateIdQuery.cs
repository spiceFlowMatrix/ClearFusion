using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetCandidateDetailsByCandidateIdQuery: IRequest<ApiResponse>
    {
        public long CandidateId { get; set; }
    }
}
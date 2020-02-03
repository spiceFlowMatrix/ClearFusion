using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetCandidateAllDetailByCandidateIdQuery : IRequest<ApiResponse>
    {
        public int CandidateId { get; set; }
    }
}
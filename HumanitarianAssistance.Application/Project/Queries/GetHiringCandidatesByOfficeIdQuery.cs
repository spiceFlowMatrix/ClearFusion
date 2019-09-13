using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetHiringCandidatesByOfficeIdQuery : IRequest<ApiResponse>
    {
        public int OfficeId { get; set; }
    }
}

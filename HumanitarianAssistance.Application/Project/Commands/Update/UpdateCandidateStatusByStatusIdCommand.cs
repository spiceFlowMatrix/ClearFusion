using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class UpdateCandidateStatusByStatusIdCommand: BaseModel, IRequest<ApiResponse>
    {
        public int statusId { get; set; }
        public long candidateId { get; set; }
    }
}
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteCandidateDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public long HiringRequestId { get; set; }
        public long? CandidateId { get; set; }
    }
} 

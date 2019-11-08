using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class CompleteHiringRequestCommand : BaseModel, IRequest<ApiResponse>
    {
        public long[] HiringRequestId { get; set; }
    }
}
 
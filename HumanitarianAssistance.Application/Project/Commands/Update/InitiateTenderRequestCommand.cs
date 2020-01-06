using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class InitiateTenderRequestCommand : BaseModel, IRequest<ApiResponse>
    {
        public long requestId { get; set; }   
    }
}

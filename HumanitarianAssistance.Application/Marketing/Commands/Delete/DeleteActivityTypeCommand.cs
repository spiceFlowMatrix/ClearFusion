using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeleteActivityTypeCommand:BaseModel, IRequest<ApiResponse>
    {      
        public int ActivityTypeId { get; set; }
    }
}
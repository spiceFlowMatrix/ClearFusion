using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Delete
{
    public class DeleteAttendanceGroupsCommand: BaseModel,IRequest<object>
    {
        public int Id { get; set; }
        
    }
}
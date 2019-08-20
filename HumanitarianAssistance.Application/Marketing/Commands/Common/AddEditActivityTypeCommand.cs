using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditActivityTypeCommand: BaseModel, IRequest<ApiResponse>
    {
        public long? ActivityTypeId { get; set; }
        public string ActivityName { get; set; }
    }
}
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteOpeningPensionDetailsCommand: BaseModel, IRequest<ApiResponse>
    {
        public int PensionId { get; set; }
    }
}
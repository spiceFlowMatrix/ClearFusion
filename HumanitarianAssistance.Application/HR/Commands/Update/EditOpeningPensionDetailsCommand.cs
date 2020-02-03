using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditOpeningPensionDetailsCommand : BaseModel, IRequest<ApiResponse>
    {
        public int Currency { get; set; }
        public int Amount { get; set; }
        public int PensionId { get; set; }
    }
}
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeleteUnitRateCommand:BaseModel, IRequest<ApiResponse>
    {
        public int UnitRateId { get; set; }
    }
}
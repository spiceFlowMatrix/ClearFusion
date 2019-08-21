using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Delete
{
    public class DeleteHolidayDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public long HolidayId { get; set; }
    }
}
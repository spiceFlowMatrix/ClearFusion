using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllWeeklyHolidaysQuery: BaseModel, IRequest<ApiResponse>
    {
        public int OfficeId { get; set; }
    }
}
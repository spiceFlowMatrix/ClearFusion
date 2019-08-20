using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetDisabledCalenderDatesQuery: IRequest<ApiResponse>
    {
        public int OfficeId { get; set; }
    }
}
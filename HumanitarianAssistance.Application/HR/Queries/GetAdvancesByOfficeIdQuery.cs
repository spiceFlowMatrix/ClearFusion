using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAdvancesByOfficeIdQuery: IRequest<ApiResponse>
    {
        public int OfficeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAdvanceHistoryByIdQuery: IRequest<object>
    {
        public int AdvanceId { get; set; }
    }
}
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetProfessionListQuery: IRequest<object>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
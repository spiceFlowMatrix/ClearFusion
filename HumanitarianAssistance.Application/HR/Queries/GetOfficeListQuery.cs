using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetOfficeListQuery: IRequest<object>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
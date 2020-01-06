using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetProcurementDetailWithReturnsListQuery: IRequest<object>
    {
        public long Id { get; set; }
    }
}
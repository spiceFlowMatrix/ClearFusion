using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetProcurementDetailsByProcurementIdQuery: IRequest<object>
    {
        public long Id { get; set; }
    }
}
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetTransportItemDataSourceQuery: IRequest<object>
    {
        public int ItemGroupTransportType { get; set; }
    }
}
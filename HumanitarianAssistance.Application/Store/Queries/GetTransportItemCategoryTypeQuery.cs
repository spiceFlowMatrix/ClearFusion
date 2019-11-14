using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetTransportItemCategoryTypeQuery: IRequest<int?>
    {
        public long ItemId { get; set; }
    }
}
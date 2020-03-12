using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetFilteredItemListQuery: IRequest<object>
    {
         public string FilterValue { get; set; }
         public int ItemGroupId { get; set; }
    }
}
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetFilteredItemGroupListQuery: IRequest<object>
    {
         public string FilterValue { get; set; }
         public int InventoryId { get; set; }
    }
}
using MediatR;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetTransportItemDataSourceQuery: IRequest<object>
    {
        public int InventoryTypeId {get; set;}
        public long InventoryId {get; set;}
        public long ItemGroupId {get; set;}
        public long ItemId {get; set;}
    }
}
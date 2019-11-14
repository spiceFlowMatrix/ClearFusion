using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddInventoryItemsCommand : BaseModel, IRequest<ApiResponse>
    {
        public long ItemId { get; set; }
        public long ItemInventory { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public long ItemGroupId { get; set; }
        public int? ItemType { get; set; }
        public int? ItemTypeCategory { get; set; }
        

    }
}

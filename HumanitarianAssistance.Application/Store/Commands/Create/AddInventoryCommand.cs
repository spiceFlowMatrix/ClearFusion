using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddInventoryCommand : BaseModel, IRequest<ApiResponse>
    {
        public string InventoryId { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryName { get; set; }
        public string InventoryDescription { get; set; }
        public int AssetType { get; set; }
        public long InventoryDebitAccount { get; set; }
        public long? InventoryCreditAccount { get; set; }
    }
}

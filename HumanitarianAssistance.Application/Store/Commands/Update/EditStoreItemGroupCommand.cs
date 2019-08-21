using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditStoreItemGroupCommand : BaseModel, IRequest<ApiResponse>
    {
        public long ItemGroupId { get; set; }
        public string InventoryId { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemGroupCode { get; set; }
        public string Description { get; set; }
    }
}

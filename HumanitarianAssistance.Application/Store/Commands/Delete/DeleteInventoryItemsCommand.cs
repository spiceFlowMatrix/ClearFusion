using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeleteInventoryItemsCommand : BaseModel, IRequest<ApiResponse>
    {
        public long ItemId { get; set; }
    }
}

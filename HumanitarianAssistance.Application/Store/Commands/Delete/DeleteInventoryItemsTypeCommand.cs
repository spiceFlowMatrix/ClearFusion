using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeleteInventoryItemsTypeCommand : BaseModel, IRequest<ApiResponse>
    {
        public int ItemType { get; set; }
    }
}

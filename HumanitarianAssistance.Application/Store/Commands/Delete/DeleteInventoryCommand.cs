using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeleteInventoryCommand : BaseModel, IRequest<ApiResponse>
    {
        public string InventoryId { get; set; } 
    }
}

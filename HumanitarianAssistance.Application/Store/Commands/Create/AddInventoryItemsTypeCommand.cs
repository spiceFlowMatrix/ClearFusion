using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddInventoryItemsTypeCommand : BaseModel, IRequest<ApiResponse>
    {
        public int ItemType { get; set; }
        public string TypeName { get; set; }
    }
}

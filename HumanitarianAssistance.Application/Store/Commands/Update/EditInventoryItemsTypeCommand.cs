using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditInventoryItemsTypeCommand : BaseModel, IRequest<ApiResponse>
    {
        public int ItemType { get; set; }
        public string TypeName { get; set; }
    }
}

using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeleteStoreSourceCodeCommand : BaseModel, IRequest<ApiResponse>
    {
        public int storeSourceCodeId { get; set; }
    }
}

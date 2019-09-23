using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeleteItemSpecificationsMasterCommand: BaseModel, IRequest<ApiResponse>
    {
        public long Id { get; set; }
    }
}
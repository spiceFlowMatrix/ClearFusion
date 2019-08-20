using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
  public  class DeletePriorityOtherDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public long PriorityOtherDetailId { get; set; }
    }
}

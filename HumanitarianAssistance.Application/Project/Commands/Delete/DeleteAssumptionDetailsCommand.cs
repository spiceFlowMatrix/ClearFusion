using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
  public  class DeleteAssumptionDetailsCommand : BaseModel, IRequest<ApiResponse>
    {
        public long AssumptionDetailId { get; set; }
    }
}

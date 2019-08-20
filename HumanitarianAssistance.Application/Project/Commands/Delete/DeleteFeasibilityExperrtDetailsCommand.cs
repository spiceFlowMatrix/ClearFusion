using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
  public class DeleteFeasibilityExperrtDetailsCommand : BaseModel, IRequest<ApiResponse>
    {
        public long ExpertOtherDetailId { get; set; }
    }
}

using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditFeasibilityExpertDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? ExpertOtherDetailId { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }
    }
}

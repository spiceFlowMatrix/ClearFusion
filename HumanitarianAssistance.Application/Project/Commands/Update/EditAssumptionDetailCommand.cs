using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Update
{
   public class EditAssumptionDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public long? AssumptionDetailId { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }
    }
}

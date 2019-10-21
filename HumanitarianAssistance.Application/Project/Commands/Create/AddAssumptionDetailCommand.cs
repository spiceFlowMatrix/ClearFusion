using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddAssumptionDetailCommand  : BaseModel, IRequest<ApiResponse>
    {
        public long? AssumptionDetailId { get; set; }
        public string Name { get; set; }
        public long ProjectId { get; set; }

    }
}

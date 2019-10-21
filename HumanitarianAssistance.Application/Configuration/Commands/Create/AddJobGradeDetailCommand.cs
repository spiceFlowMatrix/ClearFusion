using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddJobGradeDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
    }
}

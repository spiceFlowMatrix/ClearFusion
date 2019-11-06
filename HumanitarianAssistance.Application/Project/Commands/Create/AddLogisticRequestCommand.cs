using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddLogisticRequestCommand : BaseModel, IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }
        public string RequestName { get; set; }
        //public long TotalCost { get; set; }
    }
}

using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddOpportunityControlCommand : BaseModel, IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}

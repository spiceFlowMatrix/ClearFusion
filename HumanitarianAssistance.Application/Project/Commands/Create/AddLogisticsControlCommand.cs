using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddLogisticsControlCommand : BaseModel, IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}

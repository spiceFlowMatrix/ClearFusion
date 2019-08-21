using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditLogisticsControlCommand : BaseModel, IRequest<ApiResponse>
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}

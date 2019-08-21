using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Common
{
    public class AssignRoleToUserCommand : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
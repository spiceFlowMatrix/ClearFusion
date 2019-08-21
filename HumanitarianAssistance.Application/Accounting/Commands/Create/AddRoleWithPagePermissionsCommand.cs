using System.Collections.Generic;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Create
{
    public class AddRoleWithPagePermissionsCommand : IRequest<ApiResponse>
    {
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public List<ApplicationPagesModel> Permissions { get; set; }
    }
}
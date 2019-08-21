using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetPermissionsOnSelectedRoleQuery : IRequest<ApiResponse>
    {
        public string RoleId { get; set; }

    }
}
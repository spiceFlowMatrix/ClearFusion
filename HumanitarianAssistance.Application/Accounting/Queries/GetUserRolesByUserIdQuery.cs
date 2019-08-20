using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetUserRolesByUserIdQuery : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
    }
}
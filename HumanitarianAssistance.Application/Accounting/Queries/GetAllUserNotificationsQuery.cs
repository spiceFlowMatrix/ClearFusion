using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllUserNotificationsQuery : IRequest<ApiResponse>
    {
        public string UserId { get; set; }
    }
}
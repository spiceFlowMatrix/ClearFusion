using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetTransactionListByProjectIdQuery : IRequest<ApiResponse>
    {
        public long ProjectId { get; set; }
        public string UserName { get; set; } 
    }
}

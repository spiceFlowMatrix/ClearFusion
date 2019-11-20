using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetLogisticSupplierListQuery : IRequest<ApiResponse>
    {
        public long requestId { get; set; }
    }
}

using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetJobsPaginatedListQuery : IRequest<ApiResponse>
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}

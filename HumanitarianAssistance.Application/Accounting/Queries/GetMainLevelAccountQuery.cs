using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetMainLevelAccountQuery : IRequest<ApiResponse>
    {
        public long Id { get; set; }
    }
}
